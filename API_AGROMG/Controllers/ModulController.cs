using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using API_AGROMG.SimpleforDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModulController : ControllerBase
    {
        private readonly DataContext _context;

        public ModulController(DataContext context)
        {
            _context = context;
        }

        //Create New Modul
        [HttpPost]
        public async Task<ActionResult> CreateNewModul([FromBody]ModulDtos modul)
        {

            string keystring = DateTime.Now.ToString().GetHashCode().ToString("x");

            Modules _newModul = new Modules()
            {
                NumberKey = modul.NumberKey,
                NameKey = DateTime.Now.ToString() + keystring
            };

            _context.Add(_newModul);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            var createdModul = _context.Modules.FirstOrDefault(s => s.Id == _newModul.Id);

            if (createdModul==null)
            {
                return BadRequest();
            }


            foreach (var item in modul.Content)
            {
                LanguageContext newContent = new LanguageContext()
                {
                    Key = _newModul.NameKey,
                    LangUnicode = item.Languagename,
                    Context = item.Content
                };
                _context.LanguageContexts.Add(newContent);
                await _context.SaveChangesAsync();
            }

            return StatusCode(201);
        }

        //Get all List Modul

        [HttpGet]
        public async Task<ActionResult> GetModulList()
        {

            var Modules = await _context.Modules.Select(s => new Modules()
            {
                Id = s.Id,
                NumberKey = s.NumberKey,
                NameKey = _context.LanguageContexts.Where(w => w.LangUnicode == "az" && w.Key == s.NameKey).FirstOrDefault().Context,
            }).ToListAsync();

            return Ok(Modules);
        }

        // get One modul by ID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOneModul(int id)
        {
            var Modul = await _context.Modules.FirstOrDefaultAsync(s => s.Id == id);

            if (Modul == null)
            {
                return StatusCode(404);
            }

            List<LangcontentDto> langcontent = await _context.LanguageContexts.Where(s => s.Key == Modul.NameKey).Select(s => new LangcontentDto
            {
                Languagename = s.LangUnicode,
                Content = s.Context

            }).ToListAsync();

            ModulDtos findedModul = new ModulDtos()
            {
                Id = Modul.Id,
                NumberKey = Modul.NumberKey,
                Content = langcontent
            };

            return Ok(findedModul);
        }

        
        //Edit modul

        [HttpPut("{id}")]

        public async Task<ActionResult> EditModuls(int id, [FromBody]ModulDtos modul)
        {
            if (id!= modul.Id)
            {
                return BadRequest();
            }

            var editedmodul = await _context.Modules.FirstOrDefaultAsync(s => s.Id == modul.Id);

            if (editedmodul==null)
            {
                return NotFound();
            }

            editedmodul.NumberKey = modul.NumberKey;
            editedmodul.NameKey = editedmodul.NameKey;
            _context.Entry(editedmodul).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            var deletetcontent = await _context.LanguageContexts.Where(s => s.Key == editedmodul.NameKey).ToListAsync();

            foreach (var item in deletetcontent)
            {
                _context.LanguageContexts.Remove(item);
                await _context.SaveChangesAsync();
            }

            for (int i = 0; i < modul.Content.Count; i++)
            {
                LanguageContext newContent = new LanguageContext()
                {
                    Key = editedmodul.NameKey,
                    LangUnicode = modul.Content[i].Languagename,
                    Context = modul.Content[i].Content
                };
                _context.LanguageContexts.Add(newContent);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }
        
    }
}