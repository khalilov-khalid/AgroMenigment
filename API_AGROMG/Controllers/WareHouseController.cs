using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using API_AGROMG.SimpleforDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public WareHouseController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpPost]
        public async Task<ActionResult> AddWareHouse([FromBody] WareHouseDto dto)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);
            var count = await _context.WareHourses.CountAsync();
            if (count == 0) count++;
            
            foreach (var item in dto.Content)
            {
                WareHourse wareHouse = new WareHourse()
                {
                    MainId = count,
                    Name = item.Content,
                    Language = await _context.Languages.FirstOrDefaultAsync(s => s.code == item.Languagename),
                    Category = dto.Category,
                    Status = true,
                    Company = logineduser.Company,
                    
                };

                try
                {
                    await _context.WareHourses.AddAsync(wareHouse);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return StatusCode(201);
        }

        [HttpGet("{Lang}")]
        public async Task<ActionResult> GetAllWareHouse(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<StandartByCategoryDto> datalist = await _context.WareHourses.Where(s => s.Status == true && s.Language.code == lang && s.Company == logineduser.Company).Select(s => new StandartByCategoryDto()
            {
                Id=s.MainId,
                Name=s.Name,
                Category= _context.WareHouseCategories.FirstOrDefault(w=>w.Language.code==lang && w.MainId ==s.Category).Name
            }).ToListAsync();

            return Ok(datalist);
        }


        [HttpGet("{lang}/{id}")]
        public async Task<ActionResult> GetWareHouse(string lang, int id)
        {
            var datalist = await _context.WareHourses.Include(s=>s.Language).Where(s => s.MainId == id).ToListAsync();

            List<LangcontentDto> content = datalist.Select(s => new LangcontentDto()
            {
                Languagename = s.Language.code,
                Content = s.Name
            }).ToList();

            WareHouseDto data = new WareHouseDto()
            {
                Id = datalist.FirstOrDefault().Id,
                Category = datalist.FirstOrDefault().Category,
                Content = content

            };
            return Ok(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWarehouse(int id,[FromBody] WareHouseDto dto)
        {

            if (id!=dto.Id)
            {
                return BadRequest();
            }
            foreach (var item in dto.Content)
            {
                var editedData = await _context.WareHourses.FirstOrDefaultAsync(s => s.Language.code == item.Languagename && s.MainId == id);

                editedData.Category = dto.Category;
                editedData.Name = item.Content;
                try
                {
                    _context.Entry(editedData).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWareHouse(int id)
        {
            var datalist = await _context.WareHourses.Where(s => s.MainId == id).ToListAsync();

            foreach (var item in datalist)
            {
                item.Status = false;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

    }
}