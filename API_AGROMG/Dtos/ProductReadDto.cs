using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class ProductReadDto
    {
        public string Name { get; set; }
        
        public string FertilizerKind { get; set; }

        public string MeasurementUnit { get; set; }

        public string MainIngredient { get; set; }

        public string CropKind { get; set; }

        public string CropName { get; set; }

        public string CropRepredution { get; set; }

    }
}
