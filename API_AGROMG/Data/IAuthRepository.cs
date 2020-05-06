using API_AGROMG.Dtos;
using API_AGROMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public interface IAuthRepository
    {
        Task<Users> Login(string username, string password);

        Task<bool> UserExists(string username);

        Task<UserDataforLoginDto> Logineduser(int id);

        Task<Workers> VerifyUser(int id);
    }
}
