using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class ProfessionLanguange
    {
        public int Id { get; set; }

        public Profession Profession { get; set; }

        public string Name { get; set; }

        public Language Language { get; set; }
    }
}
