using API_AGROMG.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public class HRRepository : IHRRepository
    {
        private readonly DataContext _context;
        public HRRepository(DataContext context)
        {
            _context = context;
        }        

        public async Task<List<User>> GetUsers(int id)
        {
            var logineduser = await _context.Users.Include(t=>t.Company).FirstOrDefaultAsync(s => s.Id == id);

            var users = await _context.Users.Where(s => s.Status == true && s.Company == logineduser.Company).ToListAsync();

            return users;

        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);

            return user;
        }

        public async Task<List<int>> GetUserProfessions(int id)
        {
            var Userprofs = await _context.UserProfessions.Where(s => s.User.Id == id).Include(i=>i.Profession).ToListAsync();

            List<int> ListofProfID = new List<int>();
            foreach (var item in Userprofs)
            {
                ListofProfID.Add(item.Profession.Id);
            }

            return ListofProfID;
        }

        public async Task<bool> AddUser(User user, int GroupId, List<int> ProfsID)
        {
            user.PermissionsGroups = await _context.PermissionsGroups.FirstOrDefaultAsync(s => s.Id == GroupId);
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            int HumanCount = await _context.Users.Where(s => s.Company == user.Company && s.Status == true).CountAsync();

            var Company = await _context.Companies.FirstOrDefaultAsync(s => s.Id == user.Company.Id);
            Company.HumanCount = HumanCount;
            _context.Entry(Company).State = EntityState.Modified;

            foreach (var item in ProfsID)
            {
                UserProfessions userProfessions = new UserProfessions();
                userProfessions.User = user;
                userProfessions.Profession = await _context.Professions.Where(s => s.Id == item).FirstOrDefaultAsync();

                await _context.UserProfessions.AddAsync(userProfessions);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateUser(User user, int GroupId, List<int> ProfsID)
        {
            user.PermissionsGroups = await _context.PermissionsGroups.FirstOrDefaultAsync(s => s.Id == GroupId);
            try
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception)
            {
                return false;
            }

            var userprofessions = await _context.UserProfessions.Where(s => s.User.Id == user.Id).ToListAsync();

            foreach (var item in userprofessions)
            {
                _context.UserProfessions.Remove(item);
            }
            foreach (var item in ProfsID)
            {
                UserProfessions userProfessions = new UserProfessions();
                userProfessions.User = user;
                userProfessions.Profession = await _context.Professions.Where(s => s.Id == item).FirstOrDefaultAsync();

                await _context.UserProfessions.AddAsync(userProfessions);
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var deletedUser = await _context.Users.Include(s=>s.Company).FirstOrDefaultAsync(s => s.Id == id);

            deletedUser.Status = false;
            try
            {
                _context.Entry(deletedUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                return false;
            }
            int HumanCount = await _context.Users.Where(s => s.Company == deletedUser.Company && s.Status == true).CountAsync();

            var Company = await _context.Companies.FirstOrDefaultAsync(s => s.Id == deletedUser.Company.Id);
            Company.HumanCount = HumanCount;
            _context.Entry(Company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> CheckHumanCount(int id)
        {
            var user = await _context.Users.Include(c=>c.Company).Include(p=>p.Company.Packet).FirstOrDefaultAsync(s=>s.Id==id);

            if (user.Company.HumanCount >= user.Company.Packet.HumanCount)
            {
                return false;
            }
            return true;
        }
    }
}
