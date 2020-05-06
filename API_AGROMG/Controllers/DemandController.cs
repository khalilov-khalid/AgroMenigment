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
    public class DemandController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public DemandController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDemond([FromBody] DemandCreateDto demand)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            var product = await _context.Products.FirstOrDefaultAsync(s=>s.Id == demand.ProductId && s.Status ==true);
            if (product == null)
            {
                return BadRequest("Bele bir məhsul yoxdur.");
            }

            var parcel = await _context.Parcels.FirstOrDefaultAsync(s => s.Id == demand.ParcelId && s.Status == true);
            if (parcel == null)
            {
                return BadRequest("Bele bir Sahe yoxdur.");
            }

            var country = await _context.Country.FirstOrDefaultAsync(s => s.Id == demand.CountryId);
            if (country == null)
            {
                return BadRequest("Bele bir Ölkə yoxdur.");
            }

            Demand _newDemand = new Demand()
            {
                Name = demand.Name,
                Product = product,
                Quantity = demand.Quantity,
                Parcel = parcel,
                Country = country,
                Workers = logineduser,
                ExpirationDate = demand.ExpirationDate,
                RequiredDate = demand.RequiredDate,
                Company = logineduser.Company,
                CreateDate = DateTime.Now,
                CheckStatus = 1,
                Status = true                
            };

            _context.Demands.Add(_newDemand);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult> MyDemands()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<DemandReadDto> datalist = await _context.Demands.Where(s=>s.Workers == logineduser && s.Status == true).Select(s => new DemandReadDto()
            {
                Id = s.Id,
                Name= s.Name,
                CheckStatus = s.CheckStatus,
                CreateDate = s.CreateDate,
                RequiredDate = s.RequiredDate
            }).ToListAsync();
            
            return Ok(datalist);
        }


    }
}