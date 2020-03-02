using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class PermissionsGroups
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RolContent { get; set; }

        public bool Status { get; set; }

        public Company Company { get; set; }

    }
}
