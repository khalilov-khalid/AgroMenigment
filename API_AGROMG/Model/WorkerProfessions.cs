using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WorkerProfessions
    {
        public int Id { get; set; }

        public Workers Workers { get; set; }

        public Profession Profession { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime EndDate { get; set; }

        public string EndReason { get; set; }

        public bool Status { get; set; }
    }
}
