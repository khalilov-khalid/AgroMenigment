using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Model;

namespace API_AGROMG.Data
{
    public interface IPremissionGroupRepository
    {
        Task<List<PermissionsGroups>> GetAllGroups(int companyId);

        Task<PermissionsGroups> GetGroup(int id);

        Task<PermissionsGroups> AddPermission(PermissionsGroups permissions);

        Task<PermissionsGroups> UpdatePermission(PermissionsGroups permissions);

        Task<bool> DeletePermission(int id);
    }
}
