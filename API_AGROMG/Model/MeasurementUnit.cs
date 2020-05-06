using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class MeasurementUnit
    {
        public int Id { get; set; }

        public ICollection<MeasurementUnitLanguage> MeasurementUnitLanguage { get; set; }
    }
}
