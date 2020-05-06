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
    public class TemporaryAccountKindsController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporaryAccountKindsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TemporaryAccountKinds
        [HttpGet]
        public async Task<ActionResult> GetTemporaryAccountKind()
        {
            List<StandartDto> datalist = await _context.TemporaryAccountKind.Where(s => s.Status == true).Select(s=> new StandartDto() { 
                Id = s.Id,
                Name= s.Name
            }).ToListAsync();
            return Ok(datalist);
        }

        // GET: api/TemporaryAccountKinds/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemporaryAccountKind(int id)
        {
            var temporaryAccountKind = await _context.TemporaryAccountKind.FindAsync(id);            

            if (temporaryAccountKind == null)
            {
                return NotFound();
            }
            StandartDto data = new StandartDto()
            {
                Id = temporaryAccountKind.Id,
                Name = temporaryAccountKind.Name
            };

            return Ok(data);
        }

        // PUT: api/TemporaryAccountKinds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporaryAccountKind(int id, TemporaryAccountKind temporaryAccountKind)
        {
            if (id != temporaryAccountKind.Id)
            {
                return BadRequest();
            }
            temporaryAccountKind.Status = true;

            _context.Entry(temporaryAccountKind).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporaryAccountKindExists(id))
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

        // POST: api/TemporaryAccountKinds
        [HttpPost]
        public async Task<ActionResult> PostTemporaryAccountKind(TemporaryAccountKind temporaryAccountKind)
        {
            temporaryAccountKind.Status = true;
            _context.TemporaryAccountKind.Add(temporaryAccountKind);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporaryAccountKinds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TemporaryAccountKind>> DeleteTemporaryAccountKind(int id)
        {
            var temporaryAccountKind = await _context.TemporaryAccountKind.FindAsync(id);
            if (temporaryAccountKind == null)
            {
                return NotFound();
            }
            temporaryAccountKind.Status = false;
            _context.Entry(temporaryAccountKind).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TemporaryAccountKindExists(int id)
        {
            return _context.TemporaryAccountKind.Any(e => e.Id == id);
        }
    }
}
