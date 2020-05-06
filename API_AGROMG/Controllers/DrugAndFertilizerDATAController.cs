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
    public class DrugAndFertilizerDATAController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public DrugAndFertilizerDATAController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpGet("MainIngredients")]
        public async Task<ActionResult> GetMainIngredients()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<StandartDto> dataList = await _context.MainIngredients.Where(s => s.Status == true && s.Company==logineduser.Company).Select(s=> new StandartDto() { 
                Id=s.Id,
                Name=s.Name
            }).ToListAsync();

            return Ok(dataList);
        }

        [HttpGet("MeasurementUnit/{lang}")]
        public async Task<ActionResult> GetMeasurementUnit(string lang)
        {
            List<StandartDto> datalist = await _context.MeasurementUnitLanguage.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.MeasurementUnit.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok();
        }        

        [HttpGet("FertilizerKind/{lang}")]
        public async Task<ActionResult> GetFertilizerKind(string lang)
        {
            List<StandartDto> datalist = await _context.FertilizerKindLanguage.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.FertilizerKind.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(datalist);
        }
    }
}