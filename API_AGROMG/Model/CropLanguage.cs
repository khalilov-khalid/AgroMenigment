using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class CropLanguage
    {
        public int Id { get; set; }

        public Crops Crops { get; set; }

        public string Name { get; set; }

        public Language Language { get; set; }
    }
}
