using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public string Industry { get; set; }

        public Country Country { get; set; }

        public City City { get; set; }

        public string Address { get; set; }

        public string ContactPerson { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AgreementNumber { get; set; }

        public DateTime AgreementDate { get; set; }

        public PaymentTerm PaymentTerm { get; set; }

        public string PaymentAmount { get; set; }

        public PaymentKind PaymentKind { get; set; }

        public string PaymentPeriod { get; set; }

        public PaymentTerm AdvancePaymentTerm { get; set; }

        public string AdvancePaymentAmount { get; set; }

        public PaymentKind AdvancePaymentKind { get; set; }

        public string AdvancePaymentPeriod { get; set; }

        public DeliveryTerm DeliveryTerm { get; set; }

        public string DeliveryPeriod { get; set; }

        public Company Company { get; set; }

        public bool Status { get; set; }

    }
}
