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
    public class MedicalStockController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public MedicalStockController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpGet("{barcode}")]
        public async Task<ActionResult> GetStockData(int barcode)
        {
            var data = await _context.MedicalStock.Include(s=>s.Fertilizer).FirstOrDefaultAsync(s => s.Barcode == barcode);

            if (data == null)
            {
                return NotFound();
            }

            MedicalStockDto dto = new MedicalStockDto()
            {
                Id = data.Id,
                Barcode = data.Barcode,
                Count = data.Count,
                NameOfDrug = data.Fertilizer.Id,
                WareHourse = data.WareHourse,
                Expirydate = data.Expirydate,
            };

            return Ok(dto);
        }


        [HttpPost("Import")]
        public async Task<ActionResult> ImportToStock([FromBody] MedicalStockOperationDto dto)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            var data = await _context.MedicalStock.FirstOrDefaultAsync(s => s.Barcode == dto.Barcode);

            if (data == null)
            {
                MedicalStock newStock = new MedicalStock()
                {
                    Barcode = dto.Barcode,
                    Count = dto.Count,
                    Fertilizer = await _context.Fertilizer.FirstOrDefaultAsync(s => s.Id == dto.NameOfDrug),
                    WareHourse = dto.WareHourse,
                    Expirydate = dto.Expirydate,
                    Company = logineduser.Company
                };

                await _context.MedicalStock.AddAsync(newStock);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                data.Count += dto.Count;
                _context.Entry(data).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost("export")]
        public async Task<ActionResult> ExportForStor([FromBody] List<MedicalStockOperationDto> dto)
        {
            foreach (var item in dto)
            {
                var data = await _context.MedicalStock.Include(s => s.Fertilizer).FirstOrDefaultAsync(s => s.Barcode == item.Barcode);
                if (data.Count < item.Count)
                {
                    return BadRequest("Anbarda " + data.Fertilizer.Name + " adlı  mal üçün daxil edilən qədər mal yoxdur");
                }
                else
                {
                    data.Count -= item.Count;
                    _context.Entry(data).State = EntityState.Modified;
                }
            }
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("List/{lang}")]
        public async Task<ActionResult> GetMedicalStock(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<MedicalListForStockDto> datalist = await _context.MedicalStock.Include(s=>s.Fertilizer).Where(s => s.Company ==logineduser.Company).Select(s => new MedicalListForStockDto()
            {
                Category=s.Fertilizer.Category,
                Name =s.Fertilizer.Name,
                MainIngredients = s.Fertilizer.MainIngredient.Name,
                Count = s.Count,
                MeasurementUnit = _context.MeasurementUnits.FirstOrDefault(w=>w.Id == s.Fertilizer.MeasurementUnit).Id.ToString(),
                Expirydate = s.Expirydate

            }).ToListAsync();

            datalist = datalist.GroupBy(s => s.Name).Select(s=> new MedicalListForStockDto() {
                Category = s.FirstOrDefault().Category,
                Name = s.FirstOrDefault().Name,
                MainIngredients = s.FirstOrDefault().MainIngredients,
                Count = s.Sum(w=>w.Count),
                MeasurementUnit = s.FirstOrDefault().MeasurementUnit,
                Expirydate = s.FirstOrDefault().Expirydate
            }).ToList();

            
            return Ok(datalist);
        }
    }
}