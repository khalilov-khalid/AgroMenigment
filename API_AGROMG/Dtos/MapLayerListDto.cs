﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MapLayerListDto
    {
        public int Id { get; set; }

        public string type { get; set; }

        public string coordinates { get; set; }

        public string parselName { get; set; }
    }
}
