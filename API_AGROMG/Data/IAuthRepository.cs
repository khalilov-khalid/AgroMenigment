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
        Task<User> Register(Company company, User user, string password, int paketid, List<int> proffesionid);

        Task<User> Login(string username, string password);


        Task<Admin> AdminLogin(string username, string password);

        Task<bool> UserExists(string username);

        Task<UserDataforLoginDto> Logineduser(int id);

        Task<User> VerifyUser(int id);
    }
}
