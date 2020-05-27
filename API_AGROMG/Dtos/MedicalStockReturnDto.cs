using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MedicalStockReturnDto
    {
        public int StockId { get; set; }

        public decimal Quantity { get; set; }

        public int WareHouseId { get; set; }
    }

}
