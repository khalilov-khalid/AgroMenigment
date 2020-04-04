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
    public class MedicalStockDataController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public MedicalStockDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpGet("Drug/{CategoryId}")]
        public async Task<ActionResult> GetNameOfDrug(int CategoryId)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<StandartDto> datalist = await _context.NameOfDrugs.Where(s => s.Category == CategoryId && s.Status==true && s.Company ==logineduser.Company).Select(s => new StandartDto()
            {
                Id =s.Id,
                Name= s.Name
            }).ToListAsync();

            return Ok(datalist);
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