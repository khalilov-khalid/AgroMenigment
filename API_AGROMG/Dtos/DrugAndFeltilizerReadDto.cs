using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class DrugAndFeltilizerReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MainIngredient { get; set; }

        public string MeasurementUnit { get; set; }

        public int Category { get; set; }
    }
}
