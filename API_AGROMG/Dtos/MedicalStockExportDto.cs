using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MedicalStockExportDto
    {
        public string RecipientName { get; set; }
        public List<ExportQuantityByStock> ExportQuantityByStock { get; set; }
    }

    public class ExportQuantityByStock
    {
        public int StockId { get; set; }

        public decimal Quantity { get; set; }
    }
}
