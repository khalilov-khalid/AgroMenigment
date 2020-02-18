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

            var professionlist = await _context.Professions.Where(s => s.ShowStatus == true && s.Status == true).ToListAsync();

            foreach (var prof in professionlist)
            {
                var proflanglist = await _context.LanguageContexts.Where(w => w.Key == prof.Key).ToListAsync();

                foreach (var lang in proflanglist)
                {
                    DataForLanguageDto x = new DataForLanguageDto()
                    {
                        Id= prof.Id,
                        Language = lang.LangUnicode,
                        Name = lang.Context
                    };
                    data.Professions.Add(x);
                }
            }
            var Genderlist = await _context.Genders.ToListAsync();
            
            foreach (var gender in Genderlist)
            {
                var genderlanglist = await _context.LanguageContexts.Where(w => w.Key == gender.Key).ToListAsync();
                foreach (var lang in genderlanglist)
                {
                    DataForLanguageDto y = new DataForLanguageDto()
                    {
                        Id= gender.Id,
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