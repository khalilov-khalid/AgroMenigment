using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class ProfessionController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;

        public ProfessionController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        //get profession list
        [HttpGet("{Lang}")]
        public async Task<ActionResult> GetProdessionsForAdmin(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<ProfessionForReadDto> professions = await _context.Professions.Where(x => x.Status == true && x.Company ==logineduser.Company).Select(s => new ProfessionForReadDto
            {
                Id = s.Id,
                Name = _context.LanguageContexts.Where(w => w.LangUnicode == lang && w.Key == s.Key).FirstOrDefault().Context,
                Respondent =s.Respondent
            }).ToListAsync();

            return Ok(professions);
        }

        //create new profession
        [HttpPost]
        public async Task<ActionResult> ProfessionCreate([FromBody]ProfessionsDtos professions)
        {

            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            Profession newProfession = new Profession()
            {
                Key = DateTime.Now.ToString().GetHashCode().ToString("x"),
                Respondent = professions.Respondent,
                Status = true,
                Company = logineduser.Company
            };
            _context.Professions.Add(newProfession);
            await _context.SaveChangesAsync();

            var prof = _context.Professions.FindAsync(newProfession.Id);

            if (prof==null)
            {
                return BadRequest();
            }

            foreach (var item in professions.Content)
            {
                LanguageContext newContent = new LanguageContext()
                {
                    Key = newProfession.Key,
                    LangUnicode = item.Languagename,
                    Context = item.Content
                };
                _context.LanguageContexts.Add(newContent);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        //get one profession for edir
        [HttpGet("{lang}/{id}")]
        public async Task<ActionResult> GetProfessionsForEdir(int id)
        {
            var profession = await _context.Professions.FirstOrDefaultAsync(s => s.Id == id);

            if (profession==null)
            {
                return NotFound();
            }

            List<LangcontentDto> langcontent = await _context.LanguageContexts.Where(s => s.Key == profession.Key).Select(s=> new LangcontentDto
            {
                Languagename=s.LangUnicode,
                Content=s.Context

            }).ToListAsync();

            ProfessionsDtos prop = new ProfessionsDtos()
            {
                Id = profession.Id,
                Respondent=profession.Respondent,
                Content = langcontent
            };

            return Ok(prop);
        }


        //edit profession
        [HttpPut("{id}")]
        public async Task<ActionResult> Professionforedit(int id,[FromBody]ProfessionsDtos profession)
        {
            if (id != profession.Id)
            {
                return BadRequest();
            }

            var prof = await _context.Professions.FirstOrDefaultAsync(s => s.Id == id);

            if (prof == null)
            {
                return NotFound();
            }

            prof.Respondent = profession.Respondent;
            prof.Key = prof.Key;
            prof.Status = prof.Status;
            _context.Entry(prof).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
              
            var deletetcontent = await _context.LanguageContexts.Where(s => s.Key == prof.Key).ToListAsync();

            foreach (var item in deletetcontent)
            {
                _context.LanguageContexts.Remove(item);
                await _context.SaveChangesAsync();
            }

            for (int i = 0; i < profession.Content.Count; i++)
            {
                LanguageContext newContent = new LanguageContext()
                {
                    Key = prof.Key,
                    LangUnicode = profession.Content[i].Languagename,
                    Context = profession.Content[i].Content
                };
                _context.LanguageContexts.Add(newContent);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return NoContent();
        }


        //delete profesiion
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProfession(int id)
        {
            var profession = await _context.Professions.FindAsync(id);
            if (profession == null)
            {
                return NotFound();
            }

            profession.Status = false;
            _context.Entry(profession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool ProfessionExists(int id)
        {
            return _context.Professions.Any(e => e.Id == id);
        }





    }
}