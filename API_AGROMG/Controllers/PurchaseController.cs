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
    public class PurchaseController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public PurchaseController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet]
        public async Task<ActionResult> GetDemand()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<DemandReadDto> datalist = await _context.Demands.Where(s => s.Company == logineduser.Company && s.CheckStatus ==1).Select(s => new DemandReadDto()
            {
                Id = s.Id,
                Name = s.Name,
                CheckStatus = s.CheckStatus,
                CreateDate = s.CreateDate,
                RequiredDate = s.RequiredDate
            }).ToListAsync();

            return Ok(datalist);
        }

        [HttpGet("{id}/{Lang}")]
        public async Task<ActionResult> GetDemandDetals(int id,string lang)
        {
            var demand = await _context.Demands
                .Include(s=>s.Product)
                .Include(s=>s.Parcel)
                .Include(s=>s.Country)
                .Include(s=>s.Workers).FirstOrDefaultAsync(s=>s.Id == id);
            if (demand == null)
            {
                return BadRequest("Bele bir melumat yoxdur");
            }

            ProductReadDto product = await _context.Products.Where(s => s.Id == demand.Product.Id).Select(s => new ProductReadDto()
            {
                Name = s.Name,
                FertilizerKind = s.FertilizerKind.FertilizerKindLanguage.FirstOrDefault(w=>w.Language.code== lang).Name,
                MainIngredient =s.MainIngredient.Name,
                MeasurementUnit = s.MeasurementUnit.MeasurementUnitLanguage.FirstOrDefault(w=>w.Language.code == lang).Name,
                CropRepredution = null,
                CropKind = null,
                CropName = null                
                
            }).FirstOrDefaultAsync();

            DemandDetailDto data = await _context.Demands.Select(s => new DemandDetailDto()
            {
                Id = s.Id,
                ProductReadDto = product,
                Country = s.Country.CountryLanguages.FirstOrDefault().Name,
                Parsel = s.Parcel.Name,
                Quantity = s.Quantity,
                CreateDate = s.CreateDate,
                ExpirationDate = s.ExpirationDate,
                RequiredDate = s.RequiredDate,
                Workers = s.Workers.Name
                
            }).FirstOrDefaultAsync(s => s.Id == id);


            return Ok(data);
        }


    }
}