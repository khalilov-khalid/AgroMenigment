using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WorkPlanTaskFertilizer
    {
        public int Id { get; set; }

        public WorkPlanTask WorkPlanTask { get; set; }

        public Product Product { get; set; }

        public decimal Quantity { get; set; }
    }
}
