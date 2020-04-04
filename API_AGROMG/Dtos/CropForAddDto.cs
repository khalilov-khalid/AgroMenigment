using API_AGROMG.SimpleforDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class CropForAddDto
    {
        [Required]
        public int CategoryId { get; set; }

        public List<LangcontentDto> Content { get; set; }
    }
}
