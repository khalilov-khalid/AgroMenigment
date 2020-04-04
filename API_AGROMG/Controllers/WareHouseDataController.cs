using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseDataController : ControllerBase
    {
        private readonly DataContext _context;

        public WareHouseDataController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("WarehouseCategory/{lang}")]
        public async Task<ActionResult> GetWarehouseCategory(string lang)
        {
            List <StandartDto> datalist = await _context.WareHouseCategories.Where(s => s.Language.code == lang).Select(s=> new StandartDto() { 
                Id=s.MainId,
                Name=s.Name
            }).ToListAsync();

            return Ok(datalist);
        }
    }
}