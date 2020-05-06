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
using Microsoft.AspNetCore.Authorization;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryOperationKindsController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporaryOperationKindsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TemporaryOperationKinds
        [HttpGet]
        public async Task<ActionResult> GetTemporaryOperationKind()
        {
            List<StandartDto> datalist = await _context.TemporaryOperationKind.Where(s=>s.Status == true).Select(s=> new StandartDto() { 
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            return Ok(datalist);
        }

        // GET: api/TemporaryOperationKinds/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemporaryOperationKind(int id)
        {
            var temporaryOperationKind = await _context.TemporaryOperationKind.FindAsync(id);

            StandartDto data = new StandartDto();
            data.Id = temporaryOperationKind.Id;
            data.Name = temporaryOperationKind.Name;

            if (temporaryOperationKind == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // PUT: api/TemporaryOperationKinds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporaryOperationKind(int id, TemporaryOperationKind temporaryOperationKind)
        {
            if (id != temporaryOperationKind.Id)
            {
                return BadRequest();
            }
            temporaryOperationKind.Status = true;
            _context.Entry(temporaryOperationKind).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporaryOperationKindExists(id))
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

        // POST: api/TemporaryOperationKinds
        [HttpPost]
        public async Task<ActionResult> PostTemporaryOperationKind(TemporaryOperationKind temporaryOperationKind)
        {
            temporaryOperationKind.Status = true;
            _context.TemporaryOperationKind.Add(temporaryOperationKind);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporaryOperationKinds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemporaryOperationKind(int id)
        {
            var temporaryOperationKind = await _context.TemporaryOperationKind.FindAsync(id);
            if (temporaryOperationKind == null)
            {
                return NotFound();
            }

            temporaryOperationKind.Status = false;
            _context.Entry(temporaryOperationKind).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(); ;
        }

        private bool TemporaryOperationKindExists(int id)
        {
            return _context.TemporaryOperationKind.Any(e => e.Id == id);
        }
    }
}
