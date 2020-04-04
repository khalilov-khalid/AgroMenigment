using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class MedicalStock
    {
        public int Id { get; set; }
        public int Barcode { get; set; }

        public decimal Count { get; set; }

        public NameOfDrug NameOfDrug { get; set; }

        public int WareHourse { get; set; }

        public Company Company { get; set; }

        public DateTime Expirydate { get; set; }
    }
}
