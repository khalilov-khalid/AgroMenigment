﻿using API_AGROMG.SimpleforDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class PaymentTermDto
    {
        public int Id { get; set; }
        public List<LangcontentDto> ContentForLang { get; set; }
    }
}
