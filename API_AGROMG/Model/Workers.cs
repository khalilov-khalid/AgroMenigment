using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Workers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public int Gender { get; set; }

        public int WorkStatus { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime WorkStartDate { get; set; }

        public DateTime WorkEndDate { get; set; }

        public string WorkEndReason { get; set; }

        public Company Company { get; set; }

        public string Fin { get; set; }

        public string SerialNumber { get; set; }

        public string SSN { get; set; }


    }
}
