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

            List<ProfessionForReadDto> professions = await _context.ProfessionLanguanges.Where(x => x.Profession.Status == true && x.Profession.Company ==logineduser.Company && x.Language.code == lang).Select(s => new ProfessionForReadDto
            {
                Id = s.Id,
                Name = s.Name,
                Respondent =s.Profession.Respondent
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
                ProfessionLanguange professionLang = new ProfessionLanguange()
                {
                    Name = item.Content,
                    Language = await _context.Languages.FirstOrDefaultAsync(s => s.code == item.Languagename),
                    Profession = newProfession
                };
                _context.ProfessionLanguanges.Add(professionLang);
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

            ProfessionsDtos prop = new ProfessionsDtos()
            {
                Id = profession.Id,
                Respondent = profession.Respondent,
                Content = _context.ProfessionLanguanges.Where(w => w.Profession == profession).Select(w => new SimpleforDtos.LangcontentDto()
                {
                    Languagename = w.Language.code,
                    Content = w.Name
                }).ToList()
            };

            return Ok(prop);
        }


        //edit profession
        [HttpPut("{id}")]
        public async Task<ActionResult> Professionforedit(int id,[FromBody]ProfessionsDtos profession)
        {
            if (id != profession.Id)
            {
                return BadRequest("Idler duzgun deyil");
            }

            var prof = await _context.Professions.FirstOrDefaultAsync(s => s.Id == id);

            if (prof == null)
            {
                return NotFound();
            }

            prof.Respondent = profession.Respondent;
            _context.Entry(prof).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            foreach (var item in profession.Content)
            {
                var professionLanguange = await _context.ProfessionLanguanges.FirstOrDefaultAsync(s => s.Profession == prof && s.Language.code == item.Languagename);
                professionLanguange.Name = item.Content;
                _context.Entry(professionLanguange).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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