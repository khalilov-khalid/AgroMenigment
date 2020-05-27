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
    public class MainIngredientController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public MainIngredientController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpGet]
        public async Task<ActionResult> GetMainIngredients()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var loginedUser = await _auth.VerifyUser(id);

            List<StandartDto> DataList = await _context.MainIngredients.Where(s=>s.Company == loginedUser.Company && s.Status==true).Select(w=> new StandartDto() { 
                Id=w.Id,
                Name = w.Name
            }).ToListAsync();
            return Ok(DataList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMainIngredient(int id)
        {

            StandartDto data = await _context.MainIngredients.Select(s=> new StandartDto() { 
                Id=s.Id,
                Name=s.Name
            }).FirstOrDefaultAsync(s => s.Id == id);

            if (data == null)
            {
                return StatusCode(404);
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> AddMainIngredient([FromBody] StandartDto data)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var loginedUser = await _auth.VerifyUser(id);

            MainIngredient mainIngredient = new MainIngredient()
            {
                Name= data.Name,
                Company=loginedUser.Company,
                Status = true
                
            };

            try
            {
                await _context.MainIngredients.AddAsync(mainIngredient);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMainIngredient(int id , [FromBody] StandartDto data)
        {
            if (data.Id!=id)
            {
                return BadRequest("Idler duz deyil");
            }

            var editeddata = await _context.MainIngredients.FirstOrDefaultAsync(s => s.Id== id);

            editeddata.Name = data.Name;

            
            try
            {
                _context.Entry(editeddata).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMainIngredient(int id)
        {
            var loginedid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var loginedUser = await _auth.VerifyUser(loginedid);

            var deleteddata = await _context.MainIngredients.FirstOrDefaultAsync(s => s.Id == id);

            if (deleteddata==null) return StatusCode(404);

            if (deleteddata.Company != loginedUser.Company) return BadRequest("yalnis melumat");


            deleteddata.Status = false;
            _context.Entry(deleteddata).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
           
        }

    }
}