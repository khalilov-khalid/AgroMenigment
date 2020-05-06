using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkerProfessionDto
    {
        public int WorkerId { get; set; }

        public List<WorkerProfessionEditDto> WorkerProfessions { get; set; }

    }
}
