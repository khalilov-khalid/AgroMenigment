using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class ImortStockDto
    {
        public int WaitingStockId { get; set; }

        public List<CountByBarcode> CountByBarcode { get; set; }
    }

    public class CountByBarcode
    {
        public string Barcode { get; set; }

        public decimal Quantity { get; set; }

        public DateTime ExpireDate { get; set; }

        public int WareHourseId { get; set; }

    }
}
