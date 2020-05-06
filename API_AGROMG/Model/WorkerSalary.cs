using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class WorkerSalary
    {
        public int Id { get; set; }

        public Workers Workers { get; set; }

        public decimal NetSalary { get; set; }

        public decimal GrossSalary { get; set; }

        public DateTime StartSalary { get; set; }

        public DateTime EndSalary { get; set; }

        public bool Status { get; set; }


    }
}
