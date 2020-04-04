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

        [HttpGet("packet/{lang}")]
        public async Task<ActionResult> GetProdessions(string lang)
        {
            var Packets = await _context.Packets.ToListAsync();

            List<StandartDto> datalist = new List<StandartDto>();

            foreach (var pack in Packets)
            {

                var data = await _context.LanguageContexts.FirstOrDefaultAsync(s => s.Key == pack.NameKey && s.LangUnicode ==lang);

                StandartDto dto = new StandartDto()
                {
                    Id = pack.Id,
                    Name = data.Context
                };
                datalist.Add(dto);
            } 
            return Ok(datalist);
        }
    }
}