using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Packet
    {
        public int Id { get; set; }

        public string NameKey { get; set; }

        public decimal Price { get; set; }

        public string Content { get; set; }

        public int HumanCount { get; set; }
    }
}
