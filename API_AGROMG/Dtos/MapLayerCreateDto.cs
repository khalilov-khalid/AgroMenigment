using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MapLayerCreateDto
    {
        public string type { get; set; }

        public string coordinates { get; set; }

        public int? parselId { get; set; }
    }

}
