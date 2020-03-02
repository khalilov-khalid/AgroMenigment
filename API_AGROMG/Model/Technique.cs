using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Technique
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime ProductionYear { get; set; }

        public string EnginePower { get; set; }

        public string Color { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public string EngineNumber { get; set; }

        public int TechnicalCondition { get; set; }

        public int IsBusy { get; set; }

        public bool Status { get; set; }

        public Company Company { get; set; }

        public TechniqueCategory TechniqueCategory { get; set; }
    }
}
