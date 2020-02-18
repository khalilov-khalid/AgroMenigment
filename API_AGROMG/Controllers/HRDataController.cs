using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.SimpleforDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HRDataController : ControllerBase
    {
        private readonly DataContext _context;
        public HRDataController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetProdessionsAndGender()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            DataForHRDtos data = new DataForHRDtos();

            var user = await _context.Users.Include(c=>c.Company).Include(s=>s.Company.Packet).FirstOrDefaultAsync(s => s.Id == id);

            var packetContent = JsonConvert.DeserializeObject<List<string>>(user.Company.Packet.Content);

            foreach (var item in packetContent)
            {
                var modul = await _context.Modules.FirstOrDefaultAsync(s => s.NumberKey == item);

                var modullanglist = await _context.LanguageContexts.Where(w => w.Key == modul.NameKey).ToListAsync();

                foreach (var lang in modullanglist)
                {
                    ModulDataForLanguage x = new ModulDataForLanguage()
                    {
                        NumberKey = modul.NumberKey,
                        Language = lang.LangUnicode,
                        Name = lang.Context
                    };
                    data.Moduls.Add(x);
                }
            }


            //professions
            var professionlist = await _context.Professions.Where(s => s.Status == true).ToListAsync();

            foreach (var prof in professionlist)
            {
                var proflanglist = await _context.LanguageContexts.Where(w => w.Key == prof.Key).ToListAsync();

                foreach (var lang in proflanglist)
                {
                    DataForLanguageDto x = new DataForLanguageDto()
                    {
                        Id = prof.Id,
                        Language = lang.LangUnicode,
                        Name = lang.Context
                    };
                    data.Professions.Add(x);
                }
            }


            //Gender
            var Genderlist = await _context.Genders.ToListAsync();

            foreach (var gender in Genderlist)
            {
                var genderlanglist = await _context.LanguageContexts.Where(w => w.Key == gender.Key).ToListAsync();
                foreach (var lang in genderlanglist)
                {
                    DataForLanguageDto y = new DataForLanguageDto()
                    {
                        Id = gender.Id,
                        Language = lang.LangUnicode,
                        Name = lang.Context
                    };
                    data.Genders.Add(y);
                }
            }
            return Ok(data);
        }
    }    
}