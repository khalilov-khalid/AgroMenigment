using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class PacketDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int HumanContent { get; set; }

        public List<int> ModulId { get; set; }
    }
}
