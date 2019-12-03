using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class PackageModul
    {
        public int Id { get; set; }

        public Package Package { get; set; }

        public Modul Modul { get; set; }
    }
}
