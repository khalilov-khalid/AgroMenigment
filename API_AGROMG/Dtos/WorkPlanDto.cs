using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkPlanDto
    {
        public string Name { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ParcelId { get; set; }

        public List<WorkPlanTaskDto> WorkPlanTasks { get; set; }
    }
}
