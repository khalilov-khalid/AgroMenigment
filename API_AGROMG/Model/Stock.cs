using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Stock
    {
        public int Id { get; set; }

        public int StockType { get; set; }

        public string Barcode { get; set; }

        public Purchase Purchase { get; set; }

        public Product Product { get; set; }

        public Reproduction Reproduction { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime ExpireDate { get; set; }

        public WareHourse WareHourse { get; set; }

        public bool UsedStatus { get; set; }

        public Company Company { get; set; }
    }
}
