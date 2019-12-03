using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public class ContentByLanguage
    {
        private readonly DataContext _context;

        public ContentByLanguage(DataContext context)
        {
            _context = context;
        }

        public  string GetContentLanguage(string Lang, string key)
        {
            var content = _context.LanguageContexts.FirstOrDefault(s=>s.Key==key && s.LangUnicode==Lang).Context;

            return content;
        }
    }
}
