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
    public class ParcelController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public ParcelController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> CreateParcel([FromBody] ParcelForCreateDto parcel)
        {
            Parcel newparcel = new Parcel()
            {
                Name = parcel.Name,
                ParcelCategory = await _context.ParcelCategories.FirstOrDefaultAsync(s => s.Id == parcel.ParcelCategoryId),
                Area = parcel.Area,
                Crops = await _context.Crops.FirstOrDefaultAsync(s => s.Id == parcel.CropId)
            };

            try
            {
                await _context.Parcels.AddAsync(newparcel);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return StatusCode(201);
        }


        [HttpGet("{lang}")]
        public async Task<ActionResult> ReadAllParcel(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<ParcelForReadDto> datalist = await _context.Parcels.Where(s=>s.Status ==true && s.Company ==logineduser.Company).Select(s => new ParcelForReadDto()
            {
                Id = s.Id,
                ParcelCategory = _context.ParcelCategoryLanguages.FirstOrDefault(r=>r.ParcelCategory ==s.ParcelCategory && r.Language.code ==lang ).Name,
                Name = s.Name,
                Area =s.Area.ToString(),
                Crop= _context.CropLanguages.FirstOrDefault(r => r.Crops == s.Crops && r.Language.code == lang).Name,
            }).ToListAsync();

            return Ok(datalist);
        }


        [HttpGet("{lang}/{id}")]
        public async Task<ActionResult> GetParcel(int id)
        {
            ParcelForUpdateDto data = await _context.Parcels.Select(s=> new ParcelForUpdateDto() { 
                Id = s.Id,
                ParcelCategoryId = s.ParcelCategory.Id,
                Name = s.Name,
                Area= s.Area,
                CropId= s.Crops.Id
            }).FirstOrDefaultAsync(s => s.Id == id);

            return Ok(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateParcel(int id, [FromBody]ParcelForUpdateDto parcel)
        {
            if (id != parcel.Id)
            {
                return BadRequest("Idler uygun gelmir");
            }
            var editedparcel = await _context.Parcels.FirstOrDefaultAsync(s => s.Id == id);
            editedparcel.ParcelCategory = await _context.ParcelCategories.FirstOrDefaultAsync(s => s.Id == parcel.ParcelCategoryId);
            editedparcel.Name = parcel.Name;
            editedparcel.Area = parcel.Area;
            editedparcel.Crops = await _context.Crops.FirstOrDefaultAsync(s => s.Id == parcel.CropId);

            _context.Entry(editedparcel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteParcel(int id)
        {
            var deletedCrop = await _context.Parcels.FirstOrDefaultAsync(s => s.Id == id);
            deletedCrop.Status = false;
            _context.Entry(deletedCrop).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}