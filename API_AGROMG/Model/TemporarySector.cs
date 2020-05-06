using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class TemporarySector
    {
        public int Id { get; set; }

        public TemporaryParcel TemporaryParcel { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
