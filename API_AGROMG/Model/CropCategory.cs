﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class CropCategory
    {
        public int Id { get; set; }

        public ICollection<CropCategoryLanguage> CropCategoryLanguages { get; set; }
    }
}
