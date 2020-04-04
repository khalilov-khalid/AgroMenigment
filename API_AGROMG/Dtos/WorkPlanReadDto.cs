using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkPlanReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime Enddate { get; set; }

        public DateTime FinishDate { get; set; }

        public string WorkStatus { get; set; }

        public string Responder { get; set; }

        public List<WorkPlanTaskDto> WorkPlanTask { get; set; }
    }
}
