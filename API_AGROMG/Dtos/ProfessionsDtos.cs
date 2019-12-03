using API_AGROMG.SimpleforDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class ProfessionsDtos
    {
        public int Id { get; set; }

        public bool Showstatus { get; set; }

        public List<LangcontentDto> Content { get; set; }
    }
}
