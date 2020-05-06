using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class TemporaryExsel
    {
        public int Id { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime Date { get; set; }

        public TemporaryOperationKind TemporaryOperationKind { get; set; }

        public TemporaryInAndOutItems TemporaryInAndOutItems { get; set; }

        public TemporaryCustomer TemporaryCustomer { get; set; }

        public TemporaryAccountKind TemporaryAccountKind { get; set; }

        public TemporaryPayAccount TemporaryPayAccount { get; set; }

        public decimal Quantity { get; set; }

        public string User { get; set; }

        public TemporarySector TemporarySector { get; set; }

        public bool Status { get; set; }
    }
}
