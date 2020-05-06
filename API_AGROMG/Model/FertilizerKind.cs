using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class FertilizerKind
    {
        public int Id { get; set; }

        public ICollection<FertilizerKindLanguage> FertilizerKindLanguage { get; set; }
    }
}
