using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class UserDataforLoginDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public List<PermissionDto> UserPermitions { get; set; }
    }
}
