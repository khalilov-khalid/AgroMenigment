using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class PaymentTerm
    {
        public int Id { get; set; }

        public bool Status { get; set; }

        public ICollection<PaymentTermLang> PaymentTermLangs { get; set; }

        public Company Company { get; set; }
    }
}
