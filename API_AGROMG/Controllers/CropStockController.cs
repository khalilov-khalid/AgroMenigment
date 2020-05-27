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
    public class CropStockController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public CropStockController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> CropInsect(CropStockInsertDto cropStock)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            var stock = await _context.Stocks.FirstOrDefaultAsync(s=>s.Barcode == cropStock.barcode);
            if (stock == null)
            {
                Stock _newStock = new Stock() { 
                    Barcode = cropStock.barcode,
                    Product = await _context.Products.FirstOrDefaultAsync(s=>s.Id == cropStock.productId),
                    Reproduction = await _context.Reproductions.FirstOrDefaultAsync(s=>s.Id == cropStock.reproductionId),
                    StockType = 2,
                    WareHourse = await _context.WareHourses.FirstOrDefaultAsync(s => s.Id == cropStock.wareHouseId),
                    Company = logineduser.Company,
                    Quantity = cropStock.quantity,
                };
                _context.Stocks.Add(_newStock);
                await _context.SaveChangesAsync();

                StockOperation operation = new StockOperation()
                {
                    Stock = _newStock,
                    Quantity = cropStock.quantity,
                    Accepter = logineduser,
                    AcceptDate = DateTime.Now,
                    Company = logineduser.Company,
                    OperationNumber = 1,  // operation type 1  medaxildir.
                    HandingPerson = cropStock.handingPerson,
                    HandingCarNumber = cropStock.handingCarNumber
                };

                _context.StockOperations.Add(operation);
                await _context.SaveChangesAsync();

            }

            return Ok();
        }


        [HttpGet("{lang}")]
        public async Task<ActionResult> GetCropStokList(string lang)
        {
            List<CropStockListDto> datalist = await _context.Stocks
                .Where(s => s.StockType == 2).Select(s => new CropStockListDto()
                {
                    barcode = s.Barcode,
                    categoryName = s.Product.Crops.CropCategory.CropCategoryLanguages.FirstOrDefault(w => w.Language.code == lang).Name,
                    cropName = s.Product.Crops.CropLanguages.FirstOrDefault(w=>w.Language.code == lang).Name,
                    cropSortName = s.Product.ProductLang.FirstOrDefault(w=>w.Language.code == lang).Name,
                    quantity = s.Quantity,
                    reproductionName = s.Reproduction.Name,
                    MeasurementUnit = "KQ"
                }).ToListAsync();
            return Ok(datalist);
        }


    }
}