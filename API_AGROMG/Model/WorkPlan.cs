using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WorkPlan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public DateTime FinisDate { get; set; }

        public Parcel Parcel { get; set; }

        public Action Action { get; set; }

        public Workers Created { get; set; }

        public Company Company { get; set; }

        public bool Status { get; set; }




    }
}
