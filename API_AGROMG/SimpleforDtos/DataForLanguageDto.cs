﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.SimpleforDtos
{
    public class DataForLanguageDto
    {
        public int Id { get; set; }

        public List<LangcontentDto> LanguageContent = new List<LangcontentDto>();

    }
}
