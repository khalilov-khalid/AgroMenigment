using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkPlanUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int RespondentId { get; set; }

        public List<WorkPlanTaskDto> WorkPlanTask { get; set; }
    }
}
