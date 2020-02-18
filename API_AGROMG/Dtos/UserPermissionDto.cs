using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class UserPermissionDto
    {
        [Required]
        public string ModulKey { get; set; }

        [Required]
        public bool CanRead { get; set; }

        [Required]
        public bool CanWrite { get; set; }
    }
}
