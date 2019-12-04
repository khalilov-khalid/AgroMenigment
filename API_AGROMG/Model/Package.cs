using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Package
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public decimal Price { get; set; }

        public int HumanCount { get; set; }

        public string ModulContent { get; set; }

        public ICollection<Company> Companies { get; set; }

    }
}
