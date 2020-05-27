using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class StockWaitingProduct
    {
        public int Id { get; set; }

        public Purchase Purchase { get; set; }

        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public Company Company { get; set; }

        public bool Status { get; set; }
    }
}
