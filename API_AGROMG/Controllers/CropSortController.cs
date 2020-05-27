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
    public class CropSortController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public CropSortController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSort([FromBody] CropSortDto cropSort)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            var crop = await _context.Crops.FirstOrDefaultAsync(s => s.Id == cropSort.CropId);
            if (crop == null) return BadRequest("Bele bir mehsul yoxdur");

            CropSort _newSort = new CropSort()
            {
                Crops = crop,
                Company = logineduser.Company,
                Status = true
            };
            _context.CropSorts.Add(_newSort);
            await _context.SaveChangesAsync();

            foreach (var item in cropSort.ContentForLang)
            {
                CropSortLang _sortLang = new CropSortLang()
                {
                    CropSort = _newSort,
                    Language = await _context.Languages.FirstOrDefaultAsync(s => s.code == item.Languagename),
                    Name = item.Content
                };
                _context.CropSortLangs.Add(_sortLang);
                await _context.SaveChangesAsync();
            }

            return StatusCode(201);
        }

        [HttpGet("{Lang}")]
        public async Task<ActionResult> GetList(string lang)
        {
            var checklang = await _context.Languages.FirstOrDefaultAsync(s => s.code == lang);
            if (checklang == null) return BadRequest("Sorugunu Duzgun gonder");
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<StandartByCategoryReadDto> datalist = await _context.CropSorts.Where(s => s.Company == logineduser.Company)
                .Select(s => new StandartByCategoryReadDto()
                {
                    Id = s.Id,
                    Name = s.CropSortLangs.FirstOrDefault(w=>w.Language.code == lang).Name,
                    Category = s.Crops.CropLanguages.FirstOrDefault(w=>w.Language.code == lang).Name
                }).ToListAsync();
            return Ok(datalist);
        }

        [HttpGet("{lang}/{Id}")]
        public async Task<ActionResult> GetSort(string lang, int id)
        {
            CropSortDto data = await _context.CropSorts.Where(s => s.Id == id).Select(s => new CropSortDto()
            {
                Id = s.Id,
                CropId = s.Crops.Id,
                ContentForLang = s.CropSortLangs.Select(w=> new SimpleforDtos.LangcontentDto() {
                    Content = w.Name,
                    Languagename = w.Language.code
                }).ToList()
            }).FirstOrDefaultAsync();
            return Ok(data);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> EditCropSort(int id, [FromBody] CropSortDto cropSort)
        {
            if (id != cropSort.Id) return BadRequest("Idler duzgun deyil");

            var editedCrop = await _context.CropSorts.FirstOrDefaultAsync(s => s.Id == id);

            if (editedCrop == null) return NotFound();

            editedCrop.Crops = await _context.Crops.FirstOrDefaultAsync(s => s.Id == cropSort.CropId);

            foreach (var item in cropSort.ContentForLang)
            {
                var croplang = await _context.CropSortLangs.Include(s => s.CropSort).FirstOrDefaultAsync(s => s.CropSort.Id == cropSort.Id && s.Language.code == item.Languagename);
                croplang.Name = item.Content;
                _context.Entry(croplang).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return Ok();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCropSort(int id)
        {
            var deletedCropSort = await _context.CropSorts.FindAsync();

            if (deletedCropSort == null) return NotFound();


            deletedCropSort.Status = false;
            _context.Entry(deletedCropSort).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}