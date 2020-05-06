using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_AGROMG.Data;
using API_AGROMG.Model;
using API_AGROMG.Dtos;

namespace API_AGROMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryExselsController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporaryExselsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TemporaryExsels
        [HttpGet]
        public async Task<ActionResult> GetTemporaryExsel()
        {
            List< TemporaryExselsReadDto > datalist = await _context.TemporaryExsel.Where(s=>s.Status == true).Select(s=> new TemporaryExselsReadDto()
            {
                Id =s.Id,
                DocumentNumber = s.DocumentNumber,
                Date = s.Date,
                TemporaryAccountKind = s.TemporaryAccountKind.Name,
                TemporaryPayAccount = s.TemporaryPayAccount.Name,
                TemporaryOperationKind = s.TemporaryOperationKind.Name,
                TemporaryInAndOutItems = s.TemporaryInAndOutItems.Name,
                TemporaryCustomer = s.TemporaryCustomer.Name,
                TemporaryParcel = s.TemporarySector.TemporaryParcel.Name,
                TemporarySector = s.TemporarySector.Name,
                Quantity = s.Quantity,
                User = s.User
            }).ToListAsync();

            return Ok(datalist);
        }

        //// GET: api/TemporaryExsels/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TemporaryExsel>> GetTemporaryExsel(int id)
        //{
        //    var temporaryExsel = await _context.TemporaryExsel.FindAsync(id);

        //    if (temporaryExsel == null)
        //    {
        //        return NotFound();
        //    }

        //    return temporaryExsel;
        //}

        // PUT: api/TemporaryExsels/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTemporaryExsel(int id, TemporaryExsel temporaryExsel)
        //{
        //    if (id != temporaryExsel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(temporaryExsel).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TemporaryExselExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/TemporaryExsels
        [HttpPost]
        public async Task<ActionResult> PostTemporaryExsel([FromBody]TemporaryExselDto temporaryExsel)
        {
            TemporaryExsel data = new TemporaryExsel()
            {
                Date = temporaryExsel.Date,
                DocumentNumber = temporaryExsel.DocumentNumber,
                TemporaryAccountKind = await _context.TemporaryAccountKind.FirstOrDefaultAsync(s => s.Id == temporaryExsel.TemporaryAccountKind),
                TemporaryPayAccount = await _context.TemporaryPayAccount.FirstOrDefaultAsync(s => s.Id == temporaryExsel.TemporaryPayAccount),
                TemporaryOperationKind = await _context.TemporaryOperationKind.FirstOrDefaultAsync(s => s.Id == temporaryExsel.TemporaryOperationKind),
                TemporaryInAndOutItems = await _context.TemporaryInAndOutItems.FirstOrDefaultAsync(s => s.Id == temporaryExsel.TemporaryInAndOutItems),
                TemporaryCustomer = await _context.TemporaryCustomer.FirstOrDefaultAsync(s => s.Id == temporaryExsel.TemporaryCustomer),
                TemporarySector = await _context.TemporarySector.FirstOrDefaultAsync(s => s.Id == temporaryExsel.TemporarySector),
                Quantity = temporaryExsel.Quantity,
                User = temporaryExsel.User,
                Status =true                
            };

            _context.TemporaryExsel.Add(data);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporaryExsels/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<TemporaryExsel>> DeleteTemporaryExsel(int id)
        //{
        //    var temporaryExsel = await _context.TemporaryExsel.FindAsync(id);
        //    if (temporaryExsel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TemporaryExsel.Remove(temporaryExsel);
        //    await _context.SaveChangesAsync();

        //    return temporaryExsel;
        //}

        private bool TemporaryExselExists(int id)
        {
            return _context.TemporaryExsel.Any(e => e.Id == id);
        }
    }
}
