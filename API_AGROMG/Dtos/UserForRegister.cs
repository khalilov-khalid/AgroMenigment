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
        public string CompanyEmail { get; set; }

        [Required]
        public List<string> CompanyTel { get; set; }

        [Required]
        public int PacketId { get; set; }

        public DateTime PaymentEndDate { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]       
        public string UserUsername { get; set; }
        [Required]
        public string UserPassword { get; set; }

        [Required]
        public DateTime UserBirthday { get; set; }

        [Required]
        public string UserAdress { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserTel { get; set; }

        [Required]
        public int UserGender { get; set; }

    }
}
