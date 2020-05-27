using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public string Industry { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string ContactPerson { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AgreementNumber { get; set; }

        public DateTime AgreementDate { get; set; }

        public int PaymentTermId { get; set; }

        public string PaymentAmount { get; set; }

        public int PaymentKindId { get; set; }

        public string PaymentPeriod { get; set; }

        public int AdvancePaymentTermId { get; set; }

        public string AdvancePaymentAmount { get; set; }

        public int AdvancePaymentKindId { get; set; }

        public string AdvancePaymentPeriod { get; set; }

        public int DeliveryTermId { get; set; }

        public string DeliveryPeriod { get; set; }
    }
}
