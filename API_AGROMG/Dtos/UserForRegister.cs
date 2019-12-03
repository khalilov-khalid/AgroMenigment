using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class UserForRegister
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyAdress { get; set; }

        [Required]
        public List<string> CompanyEmail { get; set; }

        [Required]
        public List<string> CompanyTel { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserUsername { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}
