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


        [HttpGet("MedicalPurchase/{lang}")]
        public async Task<ActionResult> GetMedicalPuchase(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<StockWaitingProductDto> waitingList = await _context.PurchaseProducts
                .Where(s =>s.Purchase.Company == logineduser.Company &&s.Purchase.Status == true  && s.Purchase.OpenClose == true)
                .Select(s => new StockWaitingProductDto()
                {
                    Id = s.Id,
                    SupplierName =s.Purchase.Customer.Name,
                    FertilizerKind = s.Product.FertilizerKind.FertilizerKindLanguage.FirstOrDefault(w => w.Language.code == lang).Name,
                    MainIngredient =s.Product.MainIngredient.Name,
                    MeasurementUnit = s.Product.MeasurementUnit.MeasurementUnitLanguage.FirstOrDefault(w=>w.Language.code == lang).Name,
                    ProductName = s.Product.Name,
                    Quantity = s.Quantity - s.ComingQuantity
                }).ToListAsync();
            return Ok(waitingList);
        }

        [HttpPost("Import")]
        public async Task<ActionResult> ImportToStock([FromBody] ImortStockDto importdata)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);           


            var purchaseProduct = await _context.PurchaseProducts
                .Include(s => s.Purchase).Include(s=>s.Product).
                FirstOrDefaultAsync(s => s.Id == importdata.WaitingStockId);
            if (purchaseProduct == null) return BadRequest("Bele bir atinalma senedi yoxdur.");


            var totalquantity = importdata.CountByBarcode.Sum(s => s.Quantity);
            if (purchaseProduct.Quantity < totalquantity) return BadRequest("Daxil Etdiyiniz miqdar Satinalma senedinde ki miqdardan coxdur");


            foreach (var item in importdata.CountByBarcode)
            {
                var stock = await _context.Stocks
                .Where(s => s.Barcode == item.Barcode && s.Product.Id == purchaseProduct.Product.Id)
                .FirstOrDefaultAsync();

                if (stock == null)
                {
                    Stock _newStock = new Stock()
                    {
                        Purchase = purchaseProduct.Purchase,
                        Barcode = item.Barcode,
                        Product = purchaseProduct.Product,
                        Quantity = item.Quantity,
                        WareHourse = await _context.WareHourses.FirstOrDefaultAsync(s=>s.Id == item.WareHourseId),
                        ExpireDate = item.ExpireDate,
                        Company = logineduser.Company,
                        Price = purchaseProduct.LastPrice,
                        UsedStatus = false,
                        StockType = 1 // stock type 1 olanlar derman ve gubrelerdir
                    };

                    _context.Stocks.Add(_newStock);
                    await _context.SaveChangesAsync();

                    StockOperation operation = new StockOperation()
                    {
                        Stock = _newStock,
                        Quantity = item.Quantity,
                        Accepter = logineduser,
                        AcceptDate = DateTime.Now,
                        Company = logineduser.Company,
                        OperationNumber = 1,  // operation type 1  medaxildir.                      
                    };

                    _context.StockOperations.Add(operation);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    stock.Quantity += item.Quantity;
                    _context.Entry(stock).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    StockOperation operation = new StockOperation()
                    {
                        Stock = stock,
                        Quantity = item.Quantity,
                        Accepter = logineduser,
                        AcceptDate = DateTime.Now,
                        Company = logineduser.Company,
                        OperationNumber = 1

                    };

                    _context.StockOperations.Add(operation);
                    await _context.SaveChangesAsync();
                }
            }
            purchaseProduct.ComingQuantity = totalquantity;            
            _context.Entry(purchaseProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{lang}")]
        public async Task<ActionResult> GetStockList(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<MedicalStockDto> datalist = await _context.Stocks
                .Where(s=>s.Company == logineduser.Company && s.StockType == 1)
                .Include(s=> s.Product)
                .Include(s=>s.Product.MainIngredient)
                .Include(s=>s.Product.FertilizerKind)
                .Include(s=>s.Product.MeasurementUnit)                
                .Include(s=>s.WareHourse)
                .GroupBy(s => new {s.Product.Id})
                .Select(s => new MedicalStockDto()
                {
                    FertilizerKind = _context.FertilizerKindLanguage
                        .FirstOrDefault(e=>e.FertilizerKind.Id == s.FirstOrDefault().Product.FertilizerKind.Id && e.Language.code == lang).Name,
                    MainIngredient =s.FirstOrDefault().Product.MainIngredient.Name,
                    ProductName = s.FirstOrDefault().Product.Name,
                    Quantity = s.Sum(a => a.Quantity),
                    MeasurementUnit = _context.MeasurementUnitLanguage
                        .FirstOrDefault(e => e.MeasurementUnit.Id == s.FirstOrDefault().Product.MeasurementUnit.Id && e.Language.code == lang).Name,                 

                }).ToListAsync();
            return Ok(datalist);
        }

        [HttpGet("DetailsList/{lang}")]
        public async Task<ActionResult> GetStockDetailsList(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<MedicalStockListByDetailsDto> datalist = await _context.Stocks
                .Where(s => s.Company == logineduser.Company && s.StockType == 1)
                .Include(s => s.Product)
                .Include(s => s.Product.MainIngredient)
                .Include(s => s.Product.FertilizerKind)
                .Include(s => s.Product.MeasurementUnit)
                .Include(s => s.WareHourse)
                .GroupBy(s => new {s.Barcode, s.Product.Id , s.ExpireDate , s.Price, s.UsedStatus})
                .Select(s => new MedicalStockListByDetailsDto()
                {
                    FertilizerKind = _context.FertilizerKindLanguage
                        .FirstOrDefault(e => e.FertilizerKind.Id == s.FirstOrDefault().Product.FertilizerKind.Id && e.Language.code == lang).Name,
                    MainIngredient = s.FirstOrDefault().Product.MainIngredient.Name,
                    ProductName = s.FirstOrDefault().Product.Name,
                    Quantity = s.Sum(a => a.Quantity),
                    MeasurementUnit = _context.MeasurementUnitLanguage
                        .FirstOrDefault(e => e.MeasurementUnit.Id == s.FirstOrDefault().Product.MeasurementUnit.Id && e.Language.code == lang).Name,
                    Price = s.FirstOrDefault().Price,
                    ExpireDate = s.FirstOrDefault().ExpireDate,
                    UsedStatus = s.FirstOrDefault().UsedStatus
                }).ToListAsync();
            return Ok(datalist);
        }

        [HttpGet("Operation/{lang}")]
        public async Task<ActionResult> GetStockOperation(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);
            List<MedicalStockOperationDto> datalist = await _context.StockOperations
                .Where(s => s.Company == logineduser.Company && s.Stock.StockType == 1)
                .OrderByDescending(s=>s.AcceptDate)
                .Select(s => new MedicalStockOperationDto()
                {
                    Barcode = s.Stock.Barcode,
                    FertilizerKind = s.Stock.Product.FertilizerKind.FertilizerKindLanguage
                        .FirstOrDefault(w=>w.Language.code == lang).Name,
                    MainIngredient = s.Stock.Product.MainIngredient.Name,
                    ProductName = s.Stock.Product.Name,
                    Quantity = s.Quantity,
                    MeasurementUnit = s.Stock.Product.MeasurementUnit.MeasurementUnitLanguage
                        .FirstOrDefault(w => w.Language.code == lang).Name,
                    ExpireDate =s.Stock.ExpireDate,
                    WareHourseName = s.Stock.WareHourse.Name,
                    AccepterName =s.Accepter.Name,
                    AcceptDate = s.AcceptDate,
                    OperationNumber = s.OperationNumber,
                    Recipient = s.Recipient
                }).ToListAsync();
            return Ok(datalist);
        }


        [HttpGet("Export/{lang}/{Barcode}")]
        public async Task<ActionResult> GetExportInfo(string lang ,string barcode)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<MedicalStockListByDetailsDto> datalist = await _context.Stocks
                .Where(s => s.Company == logineduser.Company && s.Barcode == barcode && s.Quantity > 0)
                .Include(s => s.Product)
                .Include(s => s.Product.MainIngredient)
                .Include(s => s.Product.FertilizerKind)
                .Include(s => s.Product.MeasurementUnit)
                .Include(s => s.WareHourse).OrderBy(s=>s.UsedStatus)
                .Select(s => new MedicalStockListByDetailsDto()
                {
                    Id = s.Id,
                    FertilizerKind = _context.FertilizerKindLanguage
                    .FirstOrDefault(e => e.FertilizerKind.Id == s.Product.FertilizerKind.Id && e.Language.code == lang).Name,
                    ExpireDate = s.ExpireDate,
                    MainIngredient = s.Product.MainIngredient.Name,
                    ProductName = s.Product.Name,
                    Quantity = s.Quantity,
                    MeasurementUnit = _context.MeasurementUnitLanguage
                    .FirstOrDefault(e => e.MeasurementUnit.Id == s.Product.MeasurementUnit.Id && e.Language.code == lang).Name,
                    Price = s.Price,
                    UsedStatus = s.UsedStatus
                    
                }).ToListAsync();
            return Ok(datalist);
        }

        //[HttpPost("Export")]
        //public async Task<ActionResult> Export([FromBody] MedicalStockExportDto export)
        //{
        //    int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        //    var logineduser = await _auth.VerifyUser(id);

        //    foreach (var item in export.ExportQuantityByStock)
        //    {
        //        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == item.StockId);
        //        if (stock.Quantity < item.Quantity)
        //        {
        //            return BadRequest("Secdiyiniz mehsuldan bu miqdarda yoxdu");
        //        }
        //    }

        //    foreach (var item in export.ExportQuantityByStock)
        //    {
        //        var stock = await _context.Stocks.Include(s=>s.Product)
        //            .Include(s=>s.WareHourse)
        //            .FirstOrDefaultAsync(s => s.Id == item.StockId);
        //        stock.Quantity -= item.Quantity;
        //        _context.Entry(stock).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        StockOperation operation = new StockOperation()
        //        {
        //            Product = stock.Product,
        //            ExpireDate = stock.ExpireDate,
        //            Barcode = stock.Barcode,
        //            Quantity = item.Quantity,
        //            WareHourse = stock.WareHourse,
        //            Accepter = logineduser,
        //            AcceptDate = DateTime.Now,
        //            Company = logineduser.Company,
        //            OperationNumber = 2,
        //            Recipient = export.RecipientName
        //        };
        //        _context.StockOperations.Add(operation);
        //        await _context.SaveChangesAsync();
        //    }
        //    return Ok();
        //}

        [HttpGet("Return/{lang}/{barcode}")]
        public async Task<ActionResult> GetReturnProduct(string lang, string barcode)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<MedicalStockListByDetailsDto> datalist = await _context.Stocks
                .Where(s => s.Company == logineduser.Company && s.Barcode == barcode && s.Quantity > 0)
                .Include(s => s.Product)
                .Include(s => s.Product.MainIngredient)
                .Include(s => s.Product.FertilizerKind)
                .Include(s => s.Product.MeasurementUnit)
                .Include(s => s.WareHourse).OrderBy(s => s.UsedStatus)
                .Select(s => new MedicalStockListByDetailsDto()
                {
                    Id = s.Id,
                    FertilizerKind = _context.FertilizerKindLanguage
                    .FirstOrDefault(e => e.FertilizerKind.Id == s.Product.FertilizerKind.Id && e.Language.code == lang).Name,
                    ExpireDate = s.ExpireDate,
                    MainIngredient = s.Product.MainIngredient.Name,
                    ProductName = s.Product.Name,
                    Quantity = s.Quantity,
                    MeasurementUnit = _context.MeasurementUnitLanguage
                    .FirstOrDefault(e => e.MeasurementUnit.Id == s.Product.MeasurementUnit.Id && e.Language.code == lang).Name,
                    Price = s.Price,
                    UsedStatus = s.UsedStatus

                }).ToListAsync();

            return Ok(datalist);
        }

        //[HttpPost("Return")]
        //public async Task<ActionResult> ReturnProduct([FromBody] List<MedicalStockReturnDto> returnProduct)
        //{
        //    int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        //    var logineduser = await _auth.VerifyUser(id);
        //    foreach (var item in returnProduct)
        //    {
        //        var stock = await _context.Stocks.FirstOrDefaultAsync(s=>s.Id == item.StockId);
        //        if (stock==null)
        //        {
        //            return BadRequest("bele bir ehtiyyat yoxdur");
        //        }
        //        var _wareHourse = await _context.WareHourses.FirstOrDefaultAsync(s => s.Id == item.WareHouseId);
        //        if (_wareHourse == null)
        //        {
        //            return BadRequest("Bele bir anbar yoxdur");
        //        }
        //    }

        //    foreach (var item in returnProduct)
        //    {
        //        var stock = await _context.Stocks.Include(s=>s.Product).FirstOrDefaultAsync(s => s.Id == item.StockId);

        //        var stockUserd = await _context.Stocks.Include(s => s.Product)
        //            .FirstOrDefaultAsync(s=> s.UsedStatus == true && s.Barcode == stock.Barcode  && s.ExpireDate == stock.ExpireDate && s.Price == stock.Price && s.Product == stock.Product);
        //        if (stockUserd == null)
        //        {
        //            Stock _newStock = new Stock()
        //            {
        //                Barcode = stock.Barcode,
        //                Product = stock.Product,
        //                Price = stock.Price,
        //                ExpireDate = stock.ExpireDate,
        //                Quantity = item.Quantity,
        //                WareHourse = _context.WareHourses.FirstOrDefault(s => s.Id == item.WareHouseId),
        //                Company = logineduser.Company,
        //                UsedStatus = true
        //            };

        //            _context.Stocks.Add(_newStock);
        //            await _context.SaveChangesAsync();

        //            StockOperation operation = new StockOperation()
        //            {
        //                Product = stock.Product,
        //                ExpireDate = stock.ExpireDate,
        //                Barcode = stock.Barcode,
        //                Quantity = item.Quantity,
        //                WareHourse = _context.WareHourses.FirstOrDefault(s => s.Id == item.WareHouseId),
        //                Accepter = logineduser,
        //                AcceptDate = DateTime.Now,
        //                Company = logineduser.Company,
        //                OperationNumber = 3
        //            };
        //            _context.StockOperations.Add(operation);
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            stockUserd.Quantity += item.Quantity;
        //            _context.Entry(stock).State = EntityState.Modified;
        //            await _context.SaveChangesAsync();

        //            StockOperation operation = new StockOperation()
        //            {
        //                Product = stockUserd.Product,
        //                ExpireDate = stockUserd.ExpireDate,
        //                Barcode = stockUserd.Barcode,
        //                Quantity = item.Quantity,
        //                WareHourse = _context.WareHourses.FirstOrDefault(s => s.Id == item.WareHouseId),
        //                Accepter = logineduser,
        //                AcceptDate = DateTime.Now,
        //                Company = logineduser.Company,
        //                OperationNumber = 3
        //            };
        //            _context.StockOperations.Add(operation);
        //            await _context.SaveChangesAsync();
        //        }
                
                
        //    }
        //    return Ok();
        //}


    }
}