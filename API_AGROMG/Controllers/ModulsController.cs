using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using API_AGROMG.SimpleforDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulsController : ControllerBase
    {
        private readonly DataContext _context;

        public  ModulsController(DataContext context)
        {
            _context = context;
        }

        //Modullarin siyahisi
        [HttpGet]
        public async Task<ActionResult> GetModuls()
        {
            List<ModulDtos> moluls = await _context.Moduls.Select(s => new ModulDtos {
                Id = s.Id,
                Content = _context.LanguageContexts.Where(w => w.Key == s.Key).Select(g => new LangcontentDto {
                    Languagename = g.LangUnicode,
                    Content=g.Context
                }).ToList()
            }).ToListAsync();

            return Ok(moluls);
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult> GetModul(int id)
        {
            var modul = await _context.Moduls.FindAsync(id);

            if (modul==null)
            {
                return NotFound();
            }

            ModulDtos moluls = await _context.Moduls.Where(o=>o.Id==id).Select(s => new ModulDtos
            {
                Id = s.Id,
                Content = _context.LanguageContexts.Where(w => w.Key == s.Key).Select(g => new LangcontentDto
                {
                    Languagename = g.LangUnicode,
                    Content = g.Context
                }).ToList()
            }).FirstOrDefaultAsync();

            return Ok(moluls);
        }

        //Edit
        [HttpPut("{id}")]
        public async Task<ActionResult> EditModul(int id, [FromBody]ModulDtos modulDtos)
        {
            if (id != modulDtos.Id)
            {
                return BadRequest();
            }

            var modul = await _context.Moduls.FirstOrDefaultAsync(s => s.Id == id);

            if (modul == null)
            {
                return NotFound();
            }

            var deletetcontent = await _context.LanguageContexts.Where(s => s.Key == modul.Key).ToListAsync();

            foreach (var item in deletetcontent)
            {
                _context.LanguageContexts.Remove(item);
                await _context.SaveChangesAsync();
            }

            foreach (var item in modulDtos.Content)
            {
                LanguageContext newContent = new LanguageContext()
                {
                    Key = modul.Key,
                    LangUnicode = item.Languagename,
                    Context = item.Content
                };
                _context.LanguageContexts.Add(newContent);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}