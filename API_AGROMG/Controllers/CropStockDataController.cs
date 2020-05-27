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
    public class CropStockDataController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public CropStockDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;

            _auth = auth;
        }

        [HttpGet("CropCategory/{lang}")]
        public async Task<ActionResult> GetCropCategory(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.CropCategories.Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.CropCategoryLanguages.FirstOrDefault(w=>w.Language.code == lang).Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("Crops/{lang}")]
        public async Task<ActionResult> GetCrops(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartByCategoryDto> data = await _context.Crops.Select(s => new StandartByCategoryDto()
            {
                Id = s.Id,
                Name = s.CropLanguages.FirstOrDefault(w => w.Language.code == lang).Name,
                CategoryId = s.CropCategory.Id
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("CropsSort/{lang}")]
        public async Task<ActionResult> GetCropSort(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartByCategoryDto> data = await _context.Products.Where(s=>s.Crops != null).Select(s => new StandartByCategoryDto()
            {
                Id = s.Id,
                Name = s.ProductLang.FirstOrDefault(w => w.Language.code == lang).Name,
                CategoryId = s.Crops.Id
            }).ToListAsync();

            return Ok(data);
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

        [HttpGet("Parcel")]
        public async Task<ActionResult> GetParcel()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<StandartByCategoryDto> data = await _context.Parcels.Where(s => s.Status == true && s.ParcelCategory != null && s.Company == logineduser.Company).Select(s => new StandartByCategoryDto()
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.ParcelCategory.Id
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("WareHouse/{lang}")]
        public async Task<ActionResult> GetWareHouse(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<StandartDto> datalist = await _context.WareHourses.Where(s => s.Language.code == lang && s.Status == true && s.Company == logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.MainId,
                Name = s.Name
            }).ToListAsync();

            return Ok(datalist);
        }
    }
}