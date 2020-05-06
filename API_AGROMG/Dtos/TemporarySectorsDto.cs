using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class TemporarySectorsDto
    {
        public int Id { get; set; }

        public int TemporaryParcelId { get; set; }

        public string Name { get; set; }    
    }
}
