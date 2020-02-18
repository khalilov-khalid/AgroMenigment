using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class UserProfessions
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Profession Profession { get; set; }
    }
}
