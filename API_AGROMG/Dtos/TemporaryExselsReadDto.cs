using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class TemporaryExselsReadDto
    {
        public int Id { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime Date { get; set; }

        public string TemporaryOperationKind { get; set; }

        public string TemporaryInAndOutItems { get; set; }

        public string TemporaryCustomer { get; set; }

        public string TemporaryAccountKind { get; set; }

        public string TemporaryPayAccount { get; set; }

        public decimal Quantity { get; set; }

        public string User { get; set; }

        public string TemporaryParcel { get; set; }

        public string TemporarySector { get; set; }
    }
}
