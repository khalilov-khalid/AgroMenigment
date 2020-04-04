using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class ActionLanguange
    {
        public int Id { get; set; }

        public Action Action { get; set; }

        public Language Language { get; set; }

        public string Name { get; set; }
    }
}
