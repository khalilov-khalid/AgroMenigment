using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkerProfessionHistoryDto
    {
        public DateTime StartDate { get; set; }

        public string ProfessionName { get; set; }

        public DateTime EndDate { get; set; }

        public string EndReason { get; set; }
    }
}
