using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTermController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public PaymentTermController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> AddPaymentTerm([FromBody] PaymentTermDto paymentTerm)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            PaymentTerm _newTerm = new PaymentTerm()
            {
                Status = true,
                Company = logineduser.Company
            };            
            _context.PaymentTerms.Add(_newTerm);
            await _context.SaveChangesAsync();
            foreach (var item in paymentTerm.ContentForLang)
            {
                PaymentTermLang termLang = new PaymentTermLang()
                {
                    PaymentTerm = _newTerm,
                    Language = await _context.Languages.FirstOrDefaultAsync(s => s.code == item.Languagename),
                    Name = item.Content,
                    Status = true,
                };
                _context.PaymentTermLangs.Add(termLang);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpGet("{lang}")]
        public async Task<ActionResult> GetPaymentTerm(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<StandartDto> datalist = await _context.PaymentTerms.Where(s => s.Company == logineduser.Company && s.Status == true).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.PaymentTermLangs.FirstOrDefault(w => w.Language.code == lang).Name
            }).ToListAsync();
            return Ok(datalist);
        }

        [HttpGet("{lang}/{Id}")]
        public async Task<ActionResult> GetPaymentTermForId(int id)
        {
            PaymentTermDto data = await _context.PaymentTerms.Where(s => s.Id == id).Select(s => new PaymentTermDto()
            {
                Id = s.Id,
                ContentForLang = s.PaymentTermLangs.Select(l => new SimpleforDtos.LangcontentDto()
                {
                    Content = l.Name,
                    Languagename = l.Language.code
                }).ToList()
            }).FirstOrDefaultAsync();
            return Ok(data);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> EditTask(int id, [FromBody] PaymentTermDto termDto)
        {
            if (id != termDto.Id)
            {
                return BadRequest("Idler duzgun deyil");
            }
            foreach (var item in termDto.ContentForLang)
            {
                var termlang = await _context.PaymentTermLangs.Include(s=>s.PaymentTerm).FirstOrDefaultAsync(s=> s.PaymentTerm.Id == termDto.Id && s.Language.code == item.Languagename);
                termlang.Name = item.Content;
                _context.Entry(termlang).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTerm(int id)
        {
            var deleteddata = await _context.PaymentTerms.FirstOrDefaultAsync(s => s.Id == id);

            if (deleteddata == null)
            {
                return StatusCode(404);
            };

            deleteddata.Status = false;
            _context.Entry(deleteddata).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}