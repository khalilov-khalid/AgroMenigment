using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WorkPlanTask
    {
        public int Id { get; set; }

        public WorkPlan WorkPlan { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public Workers Respondent { get; set; }

        public DateTime FinishDate { get; set; }

        public bool Status { get; set; }
    }
}
