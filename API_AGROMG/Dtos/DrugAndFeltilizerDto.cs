using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class DrugAndFeltilizerDto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int Category { get; set; }

        [Required]
        public int MainIngredient { get; set; }

        [Required]
        public int MeasurementUnit { get; set; }
    }
}
