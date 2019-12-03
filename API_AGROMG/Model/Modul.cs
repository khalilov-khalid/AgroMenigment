using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Modul
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public bool Status { get; set; }

        public ICollection<PackageModul> PackageModuls { get; set; }
    }
}
