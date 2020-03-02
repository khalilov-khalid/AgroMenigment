using API_AGROMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public interface IHRRepository
    {        
        Task<List<User>> GetUsers(int id);

        Task<User> GetUser(int id);

        Task<List<int>> GetUserProfessions(int id);

        Task<bool> AddUser(User user, int GroupId, List<int> ProfsID);

        Task<bool> UpdateUser(User user, int GroupId, List<int> ProfsID);

        Task<bool> DeleteUser(int id);

        Task<bool> CheckHumanCount(int id);
    }
}
