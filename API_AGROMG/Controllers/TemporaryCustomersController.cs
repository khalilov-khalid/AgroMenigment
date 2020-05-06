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
    public class TemporaryCustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporaryCustomersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TemporaryCustomers
        [HttpGet]
        public async Task<ActionResult> GetTemporaryCustomer()
        {
            List<StandartDto> datalist = await _context.TemporaryCustomer.Where(s => s.Status == true).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            return Ok(datalist);
        }

        // GET: api/TemporaryCustomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemporaryCustomer>> GetTemporaryCustomer(int id)
        {
            var temporaryCustomer = await _context.TemporaryCustomer.FindAsync(id);

            if (temporaryCustomer == null)
            {
                return NotFound();
            }
            StandartDto data = new StandartDto()
            {
                Id = temporaryCustomer.Id,
                Name = temporaryCustomer.Name
            };

            return Ok(data);
        }

        // PUT: api/TemporaryCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporaryCustomer(int id, TemporaryCustomer temporaryCustomer)
        {
            if (id != temporaryCustomer.Id)
            {
                return BadRequest();
            }

            temporaryCustomer.Status = true;
            _context.Entry(temporaryCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporaryCustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/TemporaryCustomers
        [HttpPost]
        public async Task<ActionResult> PostTemporaryCustomer(TemporaryCustomer temporaryCustomer)
        {
            temporaryCustomer.Status = true;
            _context.TemporaryCustomer.Add(temporaryCustomer);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporaryCustomers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemporaryCustomer(int id)
        {
            var temporaryCustomer = await _context.TemporaryCustomer.FindAsync(id);
            if (temporaryCustomer == null)
            {
                return NotFound();
            }
            temporaryCustomer.Status = false;
            _context.Entry(temporaryCustomer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TemporaryCustomerExists(int id)
        {
            return _context.TemporaryCustomer.Any(e => e.Id == id);
        }
    }
}
