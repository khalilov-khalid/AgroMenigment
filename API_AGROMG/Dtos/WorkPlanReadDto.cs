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

        public List<WorkPlanTaskReadDto> WorkPlanTask { get; set; }
    }

    public class WorkPlanTaskReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Respondent { get; set; }

        public DateTime FinishDate { get; set; }

        public bool Status { get; set; }

        public List<WorkPlanTaskFertilizerReadDto> WorkPlanTaskFertilizer { get; set; }
    }

    public class WorkPlanTaskFertilizerReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FertilizerKind { get; set; }

        public string MeasurementUnit { get; set; }

        public string MainIngredient { get; set; }        

        public decimal Quantity { get; set; }
    }
}
