using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class PaymentKindLanguage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PaymentKind PaymentKind { get; set; }

        public Language Language { get; set; }
    }
}
