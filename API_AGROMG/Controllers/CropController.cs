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
    public class CropController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public CropController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> AddCrop([FromBody] CropForAddDto crop)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            Crops newcrop = new Crops()
            {
                CropCategory = await _context.CropCategories.FirstOrDefaultAsync(s => s.Id == crop.CategoryId),
                Company = logineduser.Company,
                Status =true
            };
            await _context.Crops.AddAsync(newcrop);
            await _context.SaveChangesAsync();

            foreach (var item in crop.Content)
            {
                CropLanguage cropLang = new CropLanguage()
                {
                    Crops = newcrop,
                    Name = item.Content,
                    Language = await _context.Languages.FirstOrDefaultAsync(s => s.code == item.Languagename)
                };
                await _context.CropLanguages.AddAsync(cropLang);
                await _context.SaveChangesAsync();
            }
            return StatusCode(201);
        }

        [HttpGet("{lang}")]
        public async Task<ActionResult> GetAllCrops(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<StandartByCategoryDto> datalist = await _context.CropLanguages.Where(s => s.Language.code == lang && s.Crops.Status == true).Select(s => new StandartByCategoryDto()
            {
                Id = s.Crops.Id,
                Name = s.Name,
                Category = _context.CropCategoryLanguages.FirstOrDefault(w => w.Language.code == lang && w.CropCategory == s.Crops.CropCategory).Name

            }).ToListAsync();

            return Ok(datalist);
        }

        [HttpGet("{Lang}/{id}")]
        public async Task<ActionResult> GetCrop(int id)
        {
            var crop = await _context.Crops.Include(s => s.CropCategory).FirstOrDefaultAsync(s => s.Id == id);

            CropForEditDto data = new CropForEditDto()
            {
                Id = crop.Id,
                CategoryId = crop.CropCategory.Id,
                Content = _context.CropLanguages.Where(w => w.Crops == crop).Select(w => new SimpleforDtos.LangcontentDto() {
                    Languagename = w.Language.code,
                    Content = w.Name
                }).ToList()
            };
            return Ok(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCrop(int id, [FromBody] CropForEditDto crop)
        {
            if (id != crop.Id)
            {
                return BadRequest("Idler duzgun deyil");
            }

            var editedcrop = await _context.Crops.FirstOrDefaultAsync(s => s.Id == crop.Id);
            editedcrop.CropCategory = await _context.CropCategories.FirstOrDefaultAsync(s => s.Id == crop.CategoryId);
            try
            {
                _context.Entry(editedcrop).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Emeliyyatda xeta bas verdi");
            }
            
            foreach (var item in crop.Content)
            {
                var editedCropLang = await _context.CropLanguages.FirstOrDefaultAsync(s => s.Crops == editedcrop && s.Language.code == item.Languagename);
                editedCropLang.Name = item.Content;
                _context.Entry(editedCropLang).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCrop(int id)
        {
            var deletedCrop = await _context.Crops.FirstOrDefaultAsync(s => s.Id == id);
            deletedCrop.Status = false;
            _context.Entry(deletedCrop).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}