using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.SimpleforDtos
{
    public class LangcontentDto
    {
        [Required]
        public string Languagename { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
