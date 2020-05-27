using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class PurchaseListDto
    {
        public int Id { get; set; }

        public string NumberCode { get; set; }

        public CustomerMainDataReadDto Customer { get; set; }

        public bool CustomsInclude { get; set; }

        public decimal CustomsCost { get; set; }

        public bool TransportInclude { get; set; }

        public decimal TransportCost { get; set; }

        public string PaymentTermName { get; set; }

        public string PaymentKindName { get; set; }

        public string PaymentPeriod { get; set; }

        public DateTime PaymentLastDate { get; set; }

        public string DeliveryTermName { get; set; }

        public string DeliveryPeriod { get; set; }

        public DateTime ApprovedDate { get; set; }

        public string ApprovedWorkerName { get; set; }

        public List<PurchaseProductReadList> PurchaseProductList { get; set; }
    }

    public class PurchaseProductReadList
    {
        public int Id { get; set; }
        
        public ProductReadDto Product { get; set; }

        public string CountryName { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal LastPrice { get; set; }

        public decimal VAT { get; set; }
    }
}
