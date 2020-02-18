using API_AGROMG.SimpleforDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class DataForHRDtos
    {
        public List<ModulDataForLanguage> Moduls = new List<ModulDataForLanguage>();

        public List<DataForLanguageDto> Professions = new List<DataForLanguageDto>();

        public List<DataForLanguageDto> Genders = new List<DataForLanguageDto>();
    }
}
