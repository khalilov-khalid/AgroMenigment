using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class CropSort
    {
        public int Id { get; set; }

        public Crops Crops { get; set; }

        public Company Company { get; set; }

        public bool Status { get; set; }

        public ICollection<CropSortLang> CropSortLangs { get; set; }
    }
}
