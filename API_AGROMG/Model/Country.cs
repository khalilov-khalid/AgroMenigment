using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Country
    {
        public int Id { get; set; }

        public ICollection<CountryLanguage> CountryLanguages { get; set; }
    }
}
