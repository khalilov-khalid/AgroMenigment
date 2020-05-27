using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Demand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DemandNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public Company Company { get; set; }

        public int CheckStatus { get; set; }

        public Workers Created { get; set; }

        public bool Status { get; set; }

        public ICollection<DemandProduct> DemandProducts { get; set; }
    }
}
