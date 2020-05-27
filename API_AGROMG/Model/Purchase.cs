using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Purchase
    {
        public int Id { get; set; }

        public string NumberCode { get; set; }

        public Customer Customer { get; set; }

        public bool CustomsInclude { get; set; }

        public decimal CustomsCost { get; set; }

        public bool TransportInclude { get; set; }

        public decimal TransportCost { get; set; }

        public PaymentTerm PaymentTerm { get; set; }

        public PaymentKind PaymentKind { get; set; }

        public string PaymentPeriod { get; set; }

        public DateTime PaymentLastDate { get; set; }
        
        public DeliveryTerm DeliveryTerm { get; set; }

        public string DeliveryPeriod { get; set; }

        public Company Company { get; set; }

        public DateTime ApprovedDate { get; set; }

        public Workers ApprovedWorker { get; set; }

        public bool OpenClose { get; set; }

        public bool Status { get; set; }

        public ICollection<PurchaseProduct> PurchaseProduct { get; set; }

    }
}
