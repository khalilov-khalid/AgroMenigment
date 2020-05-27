using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class CropStockInsertDto
    {
        public string barcode { get; set; }

        public int productId { get; set; }

        public int reproductionId { get; set; }

        public decimal quantity { get; set; }

        public int parcelId { get; set; }

        public string handingPerson { get; set; }

        public string handingCarNumber { get; set; }

        public int wareHouseId { get; set; }
    }
}
