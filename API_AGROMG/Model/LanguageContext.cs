using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class LanguageContext
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Context { get; set; }

        public string LangUnicode { get; set; }
    }
}
