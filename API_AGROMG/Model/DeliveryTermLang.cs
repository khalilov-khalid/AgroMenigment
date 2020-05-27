using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class DeliveryTermLang
    {
        public int Id { get; set; }

        public Language Language { get; set; }

        public DeliveryTerm DeliveryTerm { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
