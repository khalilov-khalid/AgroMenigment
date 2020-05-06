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


        public async Task<Users> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Username==username && x.Status == true);

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

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username)) return true;

            return false;
        }

        public async Task<UserDataforLoginDto> Logineduser(int id)
        {
            var user = await _context.Users.Include(p=>p.Workers).Include(w=>w.PermissionsGroups).FirstOrDefaultAsync(s=>s.Id==id);

            UserDataforLoginDto loginedUser = new UserDataforLoginDto()
            {
                UserId = user.Workers.Id,
                UserName = user.Workers.Name,
                UserPermitions = JsonConvert.DeserializeObject<List<PermissionDto>>(user.PermissionsGroups.RolContent)
            };

            return loginedUser;
        }

        public async Task<Workers> VerifyUser(int id)
        {
            var logineduser = await _context.Workers.Include(s => s.Company).FirstOrDefaultAsync(s => s.Id == id);

            return logineduser;
        }
    }
}
