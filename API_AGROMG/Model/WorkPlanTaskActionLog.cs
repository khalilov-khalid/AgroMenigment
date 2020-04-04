using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WorkPlanTaskActionLog
    {
        public int Id { get; set; }

        public WorkPlanActionLog WorkPlanActionLog { get; set; }

        public WorkPlanTask WorkPlanTask { get; set; }

        public string name { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime FinishDate { get; set; }

        public Action Action { get; set; }

    }
}
