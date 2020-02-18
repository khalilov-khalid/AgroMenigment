using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DataContext _context;
        public LanguageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetLanguage(string key, string lang)
        {
            var returnword = await _context.LanguageContexts.FirstOrDefaultAsync(s => s.Key == key && s.LangUnicode == lang);

            return returnword.Context;
        }
    }
}
