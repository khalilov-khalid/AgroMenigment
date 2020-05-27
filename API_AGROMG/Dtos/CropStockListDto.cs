using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class CropStockListDto
    {
        public string barcode { get; set; }

        public string categoryName { get; set; }

        public string cropName { get; set; }

        public string cropSortName { get; set; }

        public string reproductionName { get; set; }

        public decimal quantity { get; set; }

        public string MeasurementUnit { get; set; }

    }
}
