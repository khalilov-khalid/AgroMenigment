using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class ProductLang
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public Language Language { get; set; }

        public string Name { get; set; }
    }
}
