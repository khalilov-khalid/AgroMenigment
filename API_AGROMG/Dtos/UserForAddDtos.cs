﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class UserForAddDtos
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool AdminStatus { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string  Phone { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public List<int> ProfessionID { get; set; }

        public int PermissionGroupId { get; set; }

    }
}
