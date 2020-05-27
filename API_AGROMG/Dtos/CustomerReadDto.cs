using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class CustomerReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public string Industry { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string ContactPerson { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AgreementNumber { get; set; }

        public DateTime AgreementDate { get; set; }

        public string PaymentTerm { get; set; }

        public string PaymentAmount { get; set; }

        public string PaymentKind { get; set; }

        public string PaymentPeriod { get; set; }

        public string AdvancePaymentTerm { get; set; }

        public string AdvancePaymentAmount { get; set; }

        public string AdvancePaymentKind { get; set; }

        public string AdvancePaymentPeriod { get; set; }

        public string DeliveryTerm { get; set; }

        public string DeliveryPeriod { get; set; }
    }
}
