using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly DataContext _context;

        public GendersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{lang}")]
        public async Task<IActionResult> GetGender(string lang)
        {
            //var x = await _data.GetContentLanguage(languageid, "191120191234");

            var gender = await _context.Genders.Select(s => new Gender
            {
                Id = s.Id,
                Key = _context.LanguageContexts.FirstOrDefault(w => w.Key == s.Key && w.LangUnicode == lang).Context
            }).ToListAsync();

            return Ok(gender);
        }
    }
}