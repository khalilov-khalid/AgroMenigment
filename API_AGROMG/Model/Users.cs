using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Users
    {
        public int Id { get; set; }

        public Workers Workers { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public PermissionsGroups PermissionsGroups { get; set; }

        public bool Status { get; set; }
    }
}
