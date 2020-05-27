using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MedicalStockOperationDto
    {
        public string Barcode { get; set; }

        public string FertilizerKind { get; set; }

        public string MainIngredient { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public string MeasurementUnit { get; set; }

        public string WareHourseName { get; set; }

        public DateTime ExpireDate { get; set; }

        public string AccepterName { get; set; }

        public DateTime AcceptDate { get; set; }

        public int OperationNumber { get; set; }

        public string Recipient { get; set; }
    }
}
