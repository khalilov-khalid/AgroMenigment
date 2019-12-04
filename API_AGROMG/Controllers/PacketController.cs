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
using Newtonsoft.Json;

namespace API_AGROMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacketController : ControllerBase
    {
        private readonly DataContext _context;

        public PacketController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetPakets()
        {
            List<PacketDto> paketlist = await _context.Packages.Select(s => new PacketDto
            {
                Id=s.Id,
                Price=s.Price,
                HumanContent=s.HumanCount,
                ModulId= JsonConvert.DeserializeObject<List<int>>(s.ModulContent)
            }).ToListAsync();            

            return Ok(paketlist);
        }
        

    }
}