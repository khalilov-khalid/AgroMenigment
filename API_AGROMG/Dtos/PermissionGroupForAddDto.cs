using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class PermissionGroupForAddDto
    {
        public string Name { get; set; }

        public List<PermissionDto> Permissions { get; set; }
    }
}
