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
    public class RegisterController : ControllerBase
    {
        private readonly DataContext _context;

        public RegisterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetProdessionsAndGender()
        {
            DataForRegisterDtos data = new DataForRegisterDtos();

            var Packets = await _context.Packets.ToListAsync();

            foreach (var pack in Packets)
            {
                var packetlanglist = await _context.LanguageContexts.Where(s => s.Key == pack.NameKey).ToListAsync();

                DataForLanguageDto x = new DataForLanguageDto()
                {
                    Id = pack.Id
                };
                foreach (var lang in packetlanglist)
                {
                    LangcontentDto y = new LangcontentDto()
                    {
                        Languagename = lang.LangUnicode,
                        Content= lang.Context
                    };
                    x.LanguageContent.Add(y);
                    
                }
                data.Packets.Add(x);
            }

            //professions
            var professionlist = await _context.Professions.Where(s => s.ShowStatus == true && s.Status == true).ToListAsync();

            foreach (var prof in professionlist)
            {
                var proflanglist = await _context.LanguageContexts.Where(w => w.Key == prof.Key).ToListAsync();

                DataForLanguageDto x = new DataForLanguageDto()
                {
                    Id = prof.Id
                };
                foreach (var lang in proflanglist)
                {
                    LangcontentDto y = new LangcontentDto()
                    {
                        Languagename = lang.LangUnicode,
                        Content = lang.Context
                    };
                    x.LanguageContent.Add(y);

                }
                data.Professions.Add(x);
            }
            return Ok(data);
        }
    }
}