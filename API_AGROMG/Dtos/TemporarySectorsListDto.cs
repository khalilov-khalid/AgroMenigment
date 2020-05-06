using API_AGROMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class TemporarySectorsListDto
    {
        public int Id { get; set; }

        public TemporaryParcel TemporaryParcel { get; set; }

        public string Name { get; set; }
    }
}
