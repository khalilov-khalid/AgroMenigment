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

            var num = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            Demand _newDemand = new Demand()
            {
                Name = demand.Name,
                DemandNumber = num,
                Company = logineduser.Company,
                CreateDate = DateTime.Now,
                CheckStatus = 1,
                Status = true,
                Created = logineduser
            };

            _context.Demands.Add(_newDemand);
            await _context.SaveChangesAsync();

            foreach (var item in demand.DemandProduct)
            {
                DemandProduct _newDemanProduct = new DemandProduct()
                {
                    Demand = _newDemand,
                    Product = await _context.Products.FirstOrDefaultAsync(s => s.Id == item.ProductId),
                    Quantity = item.Quantity,
                    Parcel = await _context.Parcels.FirstOrDefaultAsync(s => s.Id == item.ParcelId),
                    Country = await _context.Country.FirstOrDefaultAsync(s => s.Id == item.CountryId),
                    Workers = await _context.Workers.FirstOrDefaultAsync(s => s.Id == item.RequestingWorkerId),
                    ExpirationDate = item.ExpirationDate,
                    RequiredDate = item.RequiredDate
                };
                _context.DemandProducts.Add(_newDemanProduct);
                await _context.SaveChangesAsync();
            }

            return StatusCode(201);
        }

        [HttpGet("{lang}")]
        public async Task<ActionResult> MyDemands(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<DemandReadDto> datalist = await _context.Demands.Where(s => s.Created == logineduser && s.Status == true).Select(s => new DemandReadDto()
            {
                Id = s.Id,
                Name = s.Name,
                DemandNumber = s.DemandNumber,
                CheckStatus = s.CheckStatus,
                CreateDate = s.CreateDate,     
                DemandProducts = s.DemandProducts.Select(d=> new DemandProductReadDto() {
                    Product = new ProductReadDto { 
                        FertilizerKind = d.Product.FertilizerKind.FertilizerKindLanguage.FirstOrDefault(a=>a.Language.code == lang).Name,
                        MainIngredient = d.Product.MainIngredient.Name,
                        Name = d.Product.Name,
                        MeasurementUnit =d.Product.MeasurementUnit.MeasurementUnitLanguage.FirstOrDefault(a=>a.Language.code == lang).Name,
                    },
                    Quantity  = d.Quantity,
                    Country = d.Country.CountryLanguages.FirstOrDefault(a=>a.Language.code == lang).Name,
                    Parcel = d.Parcel.Name,
                    ExpirationDate = d.ExpirationDate,
                    RequiredDate = d.RequiredDate,
                    RequestingWorker = d.Workers.Name
                }).ToList()
            }).ToListAsync();

            return Ok(datalist);
        }


    }
}