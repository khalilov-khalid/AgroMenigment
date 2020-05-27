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
    public class PurchaseController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public PurchaseController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet("Waiting/{lang}")]
        public async Task<ActionResult> GetDemand(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<DemandReadDto> datalist = await _context.Demands.Where(s => s.Company == logineduser.Company && s.CheckStatus == 1).Select(s => new DemandReadDto()
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

        [HttpGet("Approved/{lang}")]
        public async Task<ActionResult> GetApprovedDemand(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<DemandReadDto> datalist = await _context.Demands.Where(s => s.Company == logineduser.Company && s.CheckStatus == 2).Select(s => new DemandReadDto()
            {
                Id = s.Id,
                Name = s.Name,
                DemandNumber = s.DemandNumber,
                CheckStatus = s.CheckStatus,
                CreateDate = s.CreateDate,
                DemandProducts = s.DemandProducts.Select(d => new DemandProductReadDto()
                {
                    Product = new ProductReadDto
                    {
                        FertilizerKind = d.Product.FertilizerKind.FertilizerKindLanguage.FirstOrDefault(a => a.Language.code == lang).Name,
                        MainIngredient = d.Product.MainIngredient.Name,
                        Name = d.Product.Name,
                        MeasurementUnit = d.Product.MeasurementUnit.MeasurementUnitLanguage.FirstOrDefault(a => a.Language.code == lang).Name,
                    },
                    Quantity = d.Quantity,
                    Country = d.Country.CountryLanguages.FirstOrDefault(a => a.Language.code == lang).Name,
                    Parcel = d.Parcel.Name,
                    ExpirationDate = d.ExpirationDate,
                    RequiredDate = d.RequiredDate,
                    RequestingWorker = d.Workers.Name
                }).ToList()
            }).ToListAsync();

            return Ok(datalist);
        }


        [HttpPost]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseCreateDto purchase)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
                        

            Purchase _newPurchase = new Purchase()
            {
                NumberCode = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000),
                Customer = await _context.Customers.FirstOrDefaultAsync(s=>s.Id == purchase.CustomerId),
                CustomsInclude = purchase.CustomsInclude,
                CustomsCost = purchase.CustomsCost,
                TransportInclude = purchase.TransportInclude,
                TransportCost = purchase.TransportCost,                
                PaymentTerm = await _context.PaymentTerms.FirstOrDefaultAsync(s=>s.Id == purchase.PaymentTermId),
                PaymentKind = await _context.PaymentKinds.FirstOrDefaultAsync(s=>s.Id == purchase.PaymentKindId),
                PaymentPeriod = purchase.PaymentPeriod,
                PaymentLastDate = purchase.PaymentLastDate,
                DeliveryTerm = await _context.DeliveryTerms.FirstOrDefaultAsync(s=>s.Id == purchase.DeliveryTermId),
                DeliveryPeriod = purchase.DeliveryPeriod,      
                Company = logineduser.Company,
                ApprovedDate = DateTime.Now,
                ApprovedWorker = logineduser,
                OpenClose = true,
                Status = true
            };            
            _context.Purchases.Add(_newPurchase);
            await _context.SaveChangesAsync();

            foreach (var item in purchase.PurchaseProductList)
            {
                PurchaseProduct purchaseProduct = new PurchaseProduct()
                {
                    Purchase = _newPurchase,
                    Product = await _context.Products.FirstOrDefaultAsync(s => s.Id == item.ProductId),
                    Country = await _context.Country.FirstOrDefaultAsync(s => s.Id == item.CountryId),
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Discount = item.Discount,
                    LastPrice = item.LastPrice,
                    VAT = item.VAT,
                    ComingQuantity = 0
                };

                _context.PurchaseProducts.Add(purchaseProduct);
                await _context.SaveChangesAsync();
            }
            
            return StatusCode(201);
        }


        [HttpPut("Reject/{id}")]
        public async Task<ActionResult> Reject(int id)
        {
            var demand = await _context.Demands.FirstOrDefaultAsync(s => s.Id == id);

            if (demand == null)
            {
                return NotFound("bele bir teleb yoxdur");
            }
            demand.CheckStatus = 3;
            _context.Entry(demand).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Approve/{id}")]
        public async Task<ActionResult> Approve(int id)
        {
            var demand = await _context.Demands.FirstOrDefaultAsync(s => s.Id == id);

            if (demand == null)
            {
                return NotFound("bele bir teleb yoxdur");
            }
            demand.CheckStatus = 2;
            _context.Entry(demand).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("{lang}")]
        public async Task<ActionResult> ListPurchase(string lang)
        {
            List<PurchaseListDto> datalist = await _context.Purchases.Select(s => new PurchaseListDto()
            {
                Id = s.Id,
                NumberCode = s.NumberCode,
                Customer = new CustomerMainDataReadDto()
                {
                    Name = s.Customer.Name,
                    LegalName = s.Customer.LegalName,
                    Industry = s.Customer.Industry,
                    Country = s.Customer.Country.CountryLanguages.FirstOrDefault(w=>w.Language.code == lang).Name,
                    City = s.Customer.City.CityLangs.FirstOrDefault(w => w.Language.code == lang).Name,
                    Address = s.Customer.Address,
                    ContactPerson = s.Customer.ContactPerson,
                    Email = s.Customer.Email,
                    Phone = s.Customer.Phone
                },
                CustomsInclude = s.CustomsInclude,
                CustomsCost = s.CustomsCost,
                TransportInclude = s.TransportInclude,
                TransportCost = s.TransportCost,
                PaymentKindName = s.PaymentKind.PaymentKindLanguages.FirstOrDefault(w=>w.Language.code == lang).Name,
                PaymentTermName = s.PaymentTerm.PaymentTermLangs.FirstOrDefault(w=>w.Language.code == lang).Name,
                PaymentPeriod = s.PaymentPeriod,
                PaymentLastDate = s.PaymentLastDate,                
                DeliveryTermName = s.DeliveryTerm.DeliveryTermLangs.FirstOrDefault(w=>w.Language.code  == lang).Name,
                DeliveryPeriod = s.DeliveryPeriod,
                ApprovedDate = s.ApprovedDate,
                ApprovedWorkerName = s.ApprovedWorker.Name,
                PurchaseProductList = s.PurchaseProduct.Select(a=> new PurchaseProductReadList() { 
                    Id = a.Id,
                    Product = new ProductReadDto()
                    {                        
                        FertilizerKind = a.Product.FertilizerKind.FertilizerKindLanguage.FirstOrDefault(w=>w.Language.code == lang).Name,
                        MainIngredient = a.Product.MainIngredient.Name,
                        Name = a.Product.Name,
                        MeasurementUnit = a.Product.MeasurementUnit.MeasurementUnitLanguage.FirstOrDefault(w=>w.Language.code == lang).Name
                    },
                    CountryName = a.Country.CountryLanguages.FirstOrDefault(w=>w.Language.code == lang).Name,
                    Quantity = a.Quantity,
                    Price = a.Price,
                    Discount = a.Discount,
                    LastPrice = a.LastPrice,
                    VAT = a.VAT
                }).ToList()
            }).OrderByDescending(s => s.Id).ToListAsync();
            return Ok(datalist);
        }
    }
}