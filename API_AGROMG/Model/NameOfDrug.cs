using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class NameOfDrug
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Category { get; set; }

        public MainIngredient MainIngredient { get; set; }

        public Company Company { get; set; }

        public int MeasurementUnit { get; set; }

        public bool Status { get; set; }
    }
}
