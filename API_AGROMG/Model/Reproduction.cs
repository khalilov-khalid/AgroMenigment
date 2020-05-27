using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Reproduction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Company Company { get; set; }

        public bool Status { get; set; }
    }
}
