using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool Status { get; set; }

        public bool AdminStatus { get; set; }

        public string RoleContent { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public DateTime Birthday { get; set; }

        public decimal Salary { get; set; }

        public Company Company { get; set; }

        public Gender Gender { get; set; }
    }
}
