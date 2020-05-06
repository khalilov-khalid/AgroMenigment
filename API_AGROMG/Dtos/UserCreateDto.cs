using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class UserCreateDto
    {
        public int WorkerId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int PermissionGroupId { get; set; }
    }
}
