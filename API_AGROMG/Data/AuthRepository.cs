using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API_AGROMG.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Admin> AdminLogin(string username, string password)
        {
            var user = await _context.Admins.FirstOrDefaultAsync(x => x.Username == username && x.Password==password);

            if (user == null) return null;

            return user;
        }


        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Username==username);

            if (user==null) return null;

            if (!VerifyPasswordHash(password, user.PasswordHash , user.PasswordSalt)) return null;

            return user;
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {                
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                   
                }
            }
            return true;
        }

        //Qeydiyyat ucun metod
        public async Task<User> Register(Company company, User user, string password, int paketid)
        {
            company.Packet = _context.Packets.Where(s => s.Id == paketid).FirstOrDefault();

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Company = company;

            List<PermissionDto> Permissions = new List<PermissionDto>();

            List<string> Paketcontent = JsonConvert.DeserializeObject<List<string>>(company.Packet.Content);

            foreach (var item in Paketcontent)
            {
                PermissionDto permosion = new PermissionDto()
                {
                    ModulKey = item,
                    CanRead = true,
                    CanWrite = true,
                    CanEdit=true,
                    CanDelete=true
                };
                Permissions.Add(permosion);
            }

            PermissionsGroups permission = new PermissionsGroups()
            {
                Name = "Admin",
                RolContent = JsonConvert.SerializeObject(Permissions),
                Status = true,
                Company = company
            };
            await _context.PermissionsGroups.AddAsync(permission);
            await _context.SaveChangesAsync();

            user.PermissionsGroups = permission;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //pasword duzetmek ucun 
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
              
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username)) return true;

            return false;
        }


        public async Task<UserDataforLoginDto> Logineduser(int id)
        {
            User user = await _context.Users.Include(p=>p.PermissionsGroups).FirstOrDefaultAsync(s=>s.Id==id);

            UserDataforLoginDto loginedUser = new UserDataforLoginDto()
            {
                UserId = user.Id,
                UserName = user.Name,
                UserPermitions = JsonConvert.DeserializeObject<List<PermissionDto>>(user.PermissionsGroups.RolContent)
            };

            return loginedUser;
        }

        public async Task<User> VerifyUser(int id)
        {
            var logineduser = await _context.Users.Include(s => s.Company).FirstOrDefaultAsync(s => s.Id == id);

            return logineduser;
        }
    }
}
