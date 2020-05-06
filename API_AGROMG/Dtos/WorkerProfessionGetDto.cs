using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkerProfessionGetDto
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public int ProfessionId { get; set; }
    }
}
