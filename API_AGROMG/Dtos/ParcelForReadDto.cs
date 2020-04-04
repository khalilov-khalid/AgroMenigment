using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class ParcelForReadDto
    {
        public int Id { get; set; }

        public string ParcelCategory { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public string Crop { get; set; }
    }
}
