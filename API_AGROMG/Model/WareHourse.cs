using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WareHourse
    {
        public int Id { get; set; }

        public int MainId { get; set; }

        public string Name { get; set; }

        public int Category { get; set; }

        public Company Company { get; set; }

        public Language Language { get; set; }

        public bool Status { get; set; }
    }
}
