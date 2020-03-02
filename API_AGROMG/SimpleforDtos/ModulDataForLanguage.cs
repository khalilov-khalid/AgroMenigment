using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.SimpleforDtos
{
    public class ModulDataForLanguage
    {
        public string NumberKey { get; set; }

        public List<LangcontentDto> LanguageContent = new List<LangcontentDto>();
    }
}
