using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CropDataController : ControllerBase
    {
        private readonly DataContext _context;
        public CropDataController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{lang}")]
        public async Task<ActionResult> GetAllCropCategory(string lang)
        {
            List<StandartDto> datalist = await _context.CropCategoryLanguages.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.CropCategory.Id,
                Name =s.Name
            }).ToListAsync();

            return Ok(datalist);
        }        
    }
}