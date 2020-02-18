using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public int HumanCount { get; set; }

        public bool Status { get; set; }

        public DateTime StatusFinishDate { get; set; }

        public Packet  Packet { get; set; }

    }
}
