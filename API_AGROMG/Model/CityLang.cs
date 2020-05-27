using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class CityLang
    {
        public int Id { get; set; }

        public City City { get; set; }

        public Language Language { get; set; }

        public string Name { get; set; }
    }
}
