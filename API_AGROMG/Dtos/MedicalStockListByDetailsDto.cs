using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MedicalStockListByDetailsDto
    {
        public int Id { get; set; }
        
        public string FertilizerKind { get; set; }

        public string MainIngredient { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public string MeasurementUnit { get; set; }

        public DateTime ExpireDate { get; set; }

        public decimal Price { get; set; }

        public bool UsedStatus { get; set; }
    }
}
