using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class PurchaseProduct
    {
        public int Id { get; set; }

        public Purchase Purchase { get; set; }

        public Product Product { get; set; }

        public Country Country { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal LastPrice { get; set; }

        public decimal VAT { get; set; }

        public decimal ComingQuantity { get; set; }
    }
}
