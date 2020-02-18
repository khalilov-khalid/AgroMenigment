using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public interface ILanguageRepository
    {
        Task<string> GetLanguage(string key, string lang);
    }
}
