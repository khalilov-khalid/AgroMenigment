using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Crops
    {
        public int Id { get; set; }

        public CropCategory CropCategory { get; set; }

        public Company Company { get; set; }

        public bool Status { get; set; }

        public ICollection<CropLanguage> CropLanguages { get; set; }
    }
}
