using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class CropCategoryLanguage
    {
        public int Id { get; set; }

        public CropCategory CropCategory { get; set; }

        public string Name { get; set; }

        public Language Language { get; set; }
    }
}
