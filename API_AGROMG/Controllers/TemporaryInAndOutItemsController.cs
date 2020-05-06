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
    public class TemporaryInAndOutItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporaryInAndOutItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TemporaryInAndOutİtems
        [HttpGet]
        public async Task<ActionResult> GetTemporaryInAndOutİtems()
        {
            List<StandartDto> datalist = await _context.TemporaryInAndOutItems.Where(s => s.Status == true).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            return Ok(datalist);
        }

        // GET: api/TemporaryInAndOutİtems/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemporaryInAndOutİtems(int id)
        {
            var temporaryInAndOutItems = await _context.TemporaryInAndOutItems.FindAsync(id);

            if (temporaryInAndOutItems == null)
            {
                return NotFound();
            }

            StandartDto data = new StandartDto()
            {
                Id = temporaryInAndOutItems.Id,
                Name = temporaryInAndOutItems.Name
            };

            return Ok(data);
        }

        // PUT: api/TemporaryInAndOutİtems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporaryInAndOutİtems(int id, TemporaryInAndOutItems temporaryInAndOutItems)
        {
            if (id != temporaryInAndOutItems.Id)
            {
                return BadRequest();
            }
            temporaryInAndOutItems.Status = true;
            _context.Entry(temporaryInAndOutItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporaryInAndOutİtemsExists(id))
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

        // POST: api/TemporaryInAndOutİtems
        [HttpPost]
        public async Task<ActionResult> PostTemporaryInAndOutİtems(TemporaryInAndOutItems temporaryInAndOutItems)
        {
            temporaryInAndOutItems.Status = true;
            _context.TemporaryInAndOutItems.Add(temporaryInAndOutItems);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporaryInAndOutİtems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemporaryInAndOutİtems(int id)
        {
            var temporaryInAndOutItems = await _context.TemporaryInAndOutItems.FindAsync(id);
            if (temporaryInAndOutItems == null)
            {
                return NotFound();
            }
            temporaryInAndOutItems.Status = false;
            _context.Entry(temporaryInAndOutItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TemporaryInAndOutİtemsExists(int id)
        {
            return _context.TemporaryInAndOutItems.Any(e => e.Id == id);
        }
    }
}
