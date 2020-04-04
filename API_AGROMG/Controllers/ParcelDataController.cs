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
    public class ParcelDataController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public ParcelDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpGet("ParcelCategory/{lang}")]
        public async Task<ActionResult> GetParcelCategory(string lang)
        {
            List<StandartDto> data = await _context.ParcelCategoryLanguages.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.ParcelCategory.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }


        [HttpGet("CropCategory/{lang}")]
        public async Task<ActionResult> GetCropCategory(string lang)
        {
            List<StandartDto> data = await _context.CropCategoryLanguages.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.CropCategory.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("Crop/{lang}/{id}")]
        public async Task<ActionResult> GetCrop(int id, string lang)
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(userid);

            List<StandartDto> data = await _context.CropLanguages.Where(s => s.Language.code == lang && s.Crops.CropCategory.Id==id && s.Crops.Company ==logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Crops.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }

        
    }
}