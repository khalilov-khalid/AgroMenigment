using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MainIngredient MainIngredient { get; set; }

        public FertilizerKind FertilizerKind { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }

        public bool Status { get; set; }

        public Company Company { get; set; }

        public ICollection<Demand> DemandList { get; set; }
    }
}
