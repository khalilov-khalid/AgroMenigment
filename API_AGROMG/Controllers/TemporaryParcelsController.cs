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
    public class TemporaryParcelsController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporaryParcelsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TemporaryParcels
        [HttpGet]
        public async Task<ActionResult> GetTemporaryParcel()
        {
            List<StandartDto> datalist = await _context.TemporaryParcel.Where(s => s.Status == true).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            return Ok(datalist);
        }

        // GET: api/TemporaryParcels/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemporaryParcel(int id)
        {
            var temporaryParcel = await _context.TemporaryParcel.FindAsync(id);

            if (temporaryParcel == null)
            {
                return NotFound();
            }
            StandartDto data = new StandartDto()
            {
                Id = temporaryParcel.Id,
                Name = temporaryParcel.Name
            };

            return Ok(data);
        }

        // PUT: api/TemporaryParcels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporaryParcel(int id, TemporaryParcel temporaryParcel)
        {
            if (id != temporaryParcel.Id)
            {
                return BadRequest();
            }
            temporaryParcel.Status = true;
            _context.Entry(temporaryParcel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporaryParcelExists(id))
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

        // POST: api/TemporaryParcels
        [HttpPost]
        public async Task<ActionResult> PostTemporaryParcel(TemporaryParcel temporaryParcel)
        {
            temporaryParcel.Status = true;
            _context.TemporaryParcel.Add(temporaryParcel);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporaryParcels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemporaryParcel(int id)
        {
            var temporaryParcel = await _context.TemporaryParcel.FindAsync(id);
            if (temporaryParcel == null)
            {
                return NotFound();
            }
            temporaryParcel.Status = false;
            _context.Entry(temporaryParcel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TemporaryParcelExists(int id)
        {
            return _context.TemporaryParcel.Any(e => e.Id == id);
        }
    }
}
