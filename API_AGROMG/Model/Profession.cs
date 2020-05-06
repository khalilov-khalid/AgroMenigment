using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Profession
    {
        public int Id { get; set; }

        public bool Respondent { get; set; }

        public bool Status { get; set; }

        public Company Company { get; set; }
    }
}
