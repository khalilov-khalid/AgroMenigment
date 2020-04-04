using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MedicalStockDto
    {
        public int Id { get; set; }

        public int Barcode { get; set; }

        public decimal Count { get; set; }

        public int NameOfDrug { get; set; }

        public int WareHourse { get; set; }

        public DateTime Expirydate { get; set; }
    }
}
