using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class DemandProduct
    {
        public int Id { get; set; }

        public Demand Demand { get; set; }

        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public Parcel Parcel { get; set; }

        public Country Country { get; set; }

        public Workers Workers { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime RequiredDate { get; set; }

    }
}
