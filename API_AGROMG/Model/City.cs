using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class City
    {
        public int Id { get; set; }

        public Country Country { get; set; }

        public ICollection<CityLang> CityLangs { get; set; }
    }
}
