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
    public class PurchaseDataController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public PurchaseDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet("Customer")]
        public async Task<ActionResult> GetCustomer()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<StandartDto> datalist = await _context.Customers.Where(s => s.Status == true).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            return Ok(datalist);
        }

        [HttpGet("PaymentKind/{lang}")]
        public async Task<ActionResult> GetPaymentKind(string lang)
        {
            List<StandartDto> datalist = await _context.PaymentKinds.Select(s => new StandartDto()
            {
                Id= s.Id,
                Name =s.PaymentKindLanguages.Where(w=>w.Language.code ==lang).FirstOrDefault().Name
            }).ToListAsync();
            return Ok(datalist);
        }

        [HttpGet("PaymentTerm/{lang}")]
        public async Task<ActionResult> GetPaymentTerm(string lang)
        {
            List<StandartDto> datalist = await _context.PaymentTerms.Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.PaymentTermLangs.Where(w => w.Language.code == lang).FirstOrDefault().Name
            }).ToListAsync();
            return Ok(datalist);
        }

        [HttpGet("DeliveryTerm/{lang}")]
        public async Task<ActionResult> GetDeliveryTerms(string lang)
        {
            List<StandartDto> datalist = await _context.DeliveryTerms.Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.DeliveryTermLangs.Where(w => w.Language.code == lang).FirstOrDefault().Name
            }).ToListAsync();
            return Ok(datalist);
        }


        [HttpGet("Feltilizer")]
        public async Task<ActionResult> GetProduct()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<ProductForFeltilizerDto> data = await _context.Products.Where(s => s.FertilizerKind != null && s.Status == true && s.Company == logineduser.Company).Select(s => new ProductForFeltilizerDto()
            {
                Id = s.Id,
                Name = s.Name,
                FertilizerKindId = s.FertilizerKind.Id,
                MainIngredientId = s.MainIngredient.Id
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("MainIngredients")]
        public async Task<ActionResult> GetMainIngredients()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.MainIngredients.Where(s => s.Status == true && s.Company == logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("FertilizerKind/{lang}")]
        public async Task<ActionResult> GetFertilizerKind(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.FertilizerKindLanguage.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }


    }
}