using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class TemporaryExselDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemporaryOperationKind { get; set; }

        public int TemporaryInAndOutItems { get; set; }

        public int TemporaryCustomer { get; set; }

        public int TemporaryAccountKind { get; set; }

        public int TemporaryPayAccount { get; set; }

        public decimal Quantity { get; set; }

        public string User { get; set; }

        public int TemporarySector { get; set; }

        public bool Status { get; set; }

        public string DocumentNumber { get; set; }
    }
}
