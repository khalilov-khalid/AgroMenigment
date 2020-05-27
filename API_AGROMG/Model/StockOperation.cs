using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class StockOperation
    {
        public int Id { get; set; }
        
        public Stock Stock { get; set; }

        public decimal Quantity { get; set; }        

        public Workers Accepter { get; set; }

        public DateTime AcceptDate { get; set; }

        public int OperationNumber { get; set; }

        public string Recipient { get; set; }

        public string HandingPerson { get; set; }

        public string HandingCarNumber { get; set; }

        public Company Company { get; set; }

    }
}
