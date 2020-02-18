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
        public async Task<User> Register(Company company, User user, string password, int paketid, int genderid, List<int> proffesionid)
        {
            company.Packet = _context.Packets.Where(s => s.Id == paketid).FirstOrDefault();

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Gender = _context.Genders.Where(s => s.Id == genderid).FirstOrDefault();
            user.Company = company;

            List<UserPermissionDto> userPermissions = new List<UserPermissionDto>();

            List<string> Paketcontent = JsonConvert.DeserializeObject<List<string>>(company.Packet.Content);

            foreach (var item in Paketcontent)
            {
                UserPermissionDto permosion = new UserPermissionDto()
                {
                    ModulKey = item,
                    CanRead = true,
                    CanWrite = true
                };
                userPermissions.Add(permosion);
            }
            user.RoleContent = JsonConvert.SerializeObject(userPermissions);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            foreach (var item in proffesionid)
            {
                UserProfessions userProfessions = new UserProfessions();
                userProfessions.User = user;
                userProfessions.Profession = await _context.Professions.Where(s => s.Id == item).FirstOrDefaultAsync();

                await _context.UserProfessions.AddAsync(userProfessions);
                await _context.SaveChangesAsync();
            }

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
            User user = await _context.Users.FirstOrDefaultAsync(s=>s.Id==id);

            List<UserPermissionDto> userroles= JsonConvert.DeserializeObject<List<UserPermissionDto>>(user.RoleContent);

            UserDataforLoginDto loginedUser = new UserDataforLoginDto()
            {
                UserId = user.Id,
                UserName = user.Name
            };

            foreach (var item in userroles)
            {

                var modul = await _context.Modules.Where(s => s.NumberKey == item.ModulKey).FirstOrDefaultAsync();

                var modullangs = await _context.LanguageContexts.Where(s => s.Key == modul.NameKey).ToListAsync();
                List<SimpleforDtos.LangcontentDto> modullangcontent = new List<SimpleforDtos.LangcontentDto>();
                foreach (var lang in modullangs)
                {
                    modullangcontent.Add(new SimpleforDtos.LangcontentDto()
                    {
                        Languagename=lang.LangUnicode,
                        Content=lang.Context
                    });
                }
                UserPermissionLanguageDtos x =new UserPermissionLanguageDtos()
                {
                    ModulKey=item.ModulKey,
                    CanRead=item.CanRead,
                    CanWrite=item.CanWrite,
                    LanguageContent= modullangcontent
                };
                loginedUser.UserPermissions.Add(x);
            }

            return loginedUser;
        }
    }
}
