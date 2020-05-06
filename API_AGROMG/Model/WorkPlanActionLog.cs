using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WorkPlanActionLog
    {
        public int Id { get; set; }

        public WorkPlan WorkPlan { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Finishdate { get; set; }

        public Workers Responder { get; set; }

        public Action Action { get; set; }

        public DateTime ActionTime { get; set; }

        public Workers PerformingUser { get; set; }
    }
}
