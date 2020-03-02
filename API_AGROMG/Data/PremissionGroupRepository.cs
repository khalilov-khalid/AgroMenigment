using API_AGROMG.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public class PremissionGroupRepository : IPremissionGroupRepository
    {
        private readonly DataContext _context;

        public PremissionGroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PermissionsGroups> AddPermission(PermissionsGroups permissions)
        {
            await _context.PermissionsGroups.AddAsync(permissions);
            await _context.SaveChangesAsync();

            return permissions;

        }
        public async Task<PermissionsGroups> UpdatePermission(PermissionsGroups permissions)
        {
            _context.Entry(permissions).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return permissions;
        }

        public async Task<bool> DeletePermission(int id)
        {
            var deletedGroup = await _context.PermissionsGroups.FirstOrDefaultAsync(s => s.Id == id);

            var userGroups = await _context.Users.Where(s => s.PermissionsGroups == deletedGroup).ToListAsync();

            if (userGroups.Count == 0)
            {
                deletedGroup.Status = false;
                _context.Entry(deletedGroup).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<PermissionsGroups>> GetAllGroups(int companyId)
        {
            
            var groupList = await _context.PermissionsGroups.Where(s => s.Company.Id == companyId && s.Status==true).ToListAsync();
            return groupList;
        }

        public async Task<PermissionsGroups> GetGroup(int id)
        {
            var group =await _context.PermissionsGroups.FirstOrDefaultAsync(p => p.Id == id);

            return group;
        }
    }
}
