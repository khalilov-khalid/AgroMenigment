using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class PurchaseCreateDto
    {
        public int CustomerId { get; set; }

        public bool CustomsInclude { get; set; }

        public decimal CustomsCost { get; set; }

        public bool TransportInclude { get; set; }

        public decimal TransportCost { get; set; }       

        public int PaymentTermId { get; set; }

        public int PaymentKindId { get; set; }

        public string PaymentPeriod { get; set; }

        public DateTime PaymentLastDate { get; set; }

        public int DeliveryTermId { get; set; }

        public string DeliveryPeriod { get; set; }

        public List<PurchaseProductList> PurchaseProductList { get; set; }
    }

    public class PurchaseProductList
    {
        public int ProductId { get; set; }

        public int CountryId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal LastPrice { get; set; }

        public decimal VAT { get; set; }
    }
}
