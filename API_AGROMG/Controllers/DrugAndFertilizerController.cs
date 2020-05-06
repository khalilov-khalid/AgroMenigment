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
    public class DrugAndFertilizerController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;

        public DrugAndFertilizerController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpPost]
        public async Task<ActionResult> AddDrugAndFeltilizer([FromBody] DrugAndFeltilizerDto data)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            var _feltizerKind = await _context.FertilizerKind.FirstOrDefaultAsync(w => w.Id == data.Category);
            if (_feltizerKind==null)
            {
                return BadRequest("Bele bir kategorya yoxdur");
            }

            var _mainIngredient = await _context.MainIngredients.FirstOrDefaultAsync(s => s.Id == data.MainIngredient);
            if (_mainIngredient == null)
            {
                return BadRequest("Bele bir tesir edici madde yoxdur");
            }
            var _measurementUnit = await _context.MeasurementUnits.FirstOrDefaultAsync(s => s.Id == data.MeasurementUnit);
            if (_measurementUnit == null)
            {
                return BadRequest("Bele bir kategorya yoxdur");
            }
            Product _product = new Product()
            {
                Name = data.Name,
                FertilizerKind = _feltizerKind,
                MainIngredient = _mainIngredient,
                MeasurementUnit = _measurementUnit,
                Company = logineduser.Company,
                Status = true
            };

            try
            {
                await _context.Products.AddAsync(_product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return StatusCode(201);
        }

        [HttpGet("{lang}")]
        public async Task<ActionResult> GetAllDrugAndFeltilizer(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<DrugAndFeltilizerReadDto> datalist = await _context.Products.Where(s => s.FertilizerKind != null && s.Status == true && s.Company == logineduser.Company).Select(s => new DrugAndFeltilizerReadDto()
            {
                Id = s.Id,
                Name = s.Name,
                MainIngredient = s.MainIngredient.Name,
                MeasurementUnit = _context.MeasurementUnitLanguage.FirstOrDefault(w => w.Language.code == lang && w.MeasurementUnit == s.MeasurementUnit).Name,
                Category = s.FertilizerKind.Id
            }).ToListAsync();

            return Ok(datalist);
        }


        [HttpGet("{lang}/{id}")]
        public async Task<ActionResult> GetAllDrugAndFeltilizer(string lang, int id)
        {
            int userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(userid);

            DrugAndFeltilizerDto data = await _context.Products.Select(s => new DrugAndFeltilizerDto()
            {
                ID = s.Id,
                Name = s.Name,
                Category = s.FertilizerKind.Id,
                MainIngredient = s.MainIngredient.Id,
                MeasurementUnit = s.MeasurementUnit.Id
            }).FirstOrDefaultAsync(w => w.ID == id);

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDrugAndFertilizer(int id, [FromBody] DrugAndFeltilizerDto data)
        {
            if (id != data.ID)
            {
                return BadRequest();
            }

            var editeddata = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);

            editeddata.Name = data.Name;
            editeddata.MainIngredient = await _context.MainIngredients.FirstOrDefaultAsync(w => w.Id == data.MainIngredient);
            editeddata.MeasurementUnit = await _context.MeasurementUnits.FirstOrDefaultAsync(w => w.Id == data.MeasurementUnit);
            editeddata.FertilizerKind = await _context.FertilizerKind.FirstOrDefaultAsync(w => w.Id == data.Category);

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
        public async Task<ActionResult> DeleteDrugAndFertilizer(int id)
        {
            var deletedData = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
            deletedData.Status = false;

            try
            {
                _context.Entry(deletedData).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }


    }
}