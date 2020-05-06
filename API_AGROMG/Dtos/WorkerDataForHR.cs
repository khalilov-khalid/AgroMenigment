using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkerDataForHR
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public DateTime StartDate { get; set; }

        public string WorkStatus { get; set; }

        public string GrossSalary { get; set; }

        public string NetSalary { get; set; }

        public int Gender { get; set; }

        public List<int> Professions { get; set; }

        public string Fin { get; set; }

        public string SerialNumber { get; set; }

        public string SSN { get; set; }
    }
}
