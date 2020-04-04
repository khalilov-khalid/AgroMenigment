using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MedicalListForStockDto
    {
        public int Category { get; set; }

        public string Name { get; set; }

        public string MainIngredients { get; set; }

        public decimal Count { get; set; }

        public string MeasurementUnit { get; set; }

        public DateTime Expirydate { get; set; }


    }
}
