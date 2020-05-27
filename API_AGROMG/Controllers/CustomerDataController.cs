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
    public class CustomerDataController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public CustomerDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;

            _auth = auth;
        }


        [HttpGet("Country/{lang}")]
        public async Task<ActionResult> GetCountries(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.Country.Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.CountryLanguages.FirstOrDefault(w=>w.Language.code == lang).Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("City/{lang}")]
        public async Task<ActionResult> GetCities(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<CitiesDto> data = await _context.Cities.Select(s => new CitiesDto()
            {
                Id = s.Id,
                Name = s.CityLangs.FirstOrDefault(w => w.Language.code == lang).Name,
                CountryId = s.Country.Id
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("PaymentTerm/{lang}")]
        public async Task<ActionResult> GetPaymentTerms( string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.PaymentTerms.Where(s => s.Status == true && s.Company == logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.PaymentTermLangs.FirstOrDefault(w => w.Language.code == lang).Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("PaymentKind/{lang}")]
        public async Task<ActionResult> GetPaymentKind(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.PaymentKinds.Where(s => s.Status == true && s.Company == logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.PaymentKindLanguages.FirstOrDefault(w => w.Language.code == lang).Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("DeliveryTerms/{lang}")]
        public async Task<ActionResult> GetDeliveryTerms(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.DeliveryTerms.Where(s => s.Status == true && s.Company == logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.DeliveryTermLangs.FirstOrDefault(w => w.Language.code == lang).Name
            }).ToListAsync();

            return Ok(data);
        }
    }
}