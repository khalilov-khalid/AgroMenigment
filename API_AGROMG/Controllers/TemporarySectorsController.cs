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
    public class TemporarySectorsController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporarySectorsController(DataContext context)
        {
            _context = context;
        }      

        // GET: api/TemporarySectors
        [HttpGet]
        public async Task<ActionResult> GetTemporarySector()
        {
            List<TemporarySectorsListDto> datalist = await _context.TemporarySector.Where(s => s.Status == true).Select(s => new TemporarySectorsListDto()
            {
                Id= s.Id,
                Name =s.Name,
                TemporaryParcel = s.TemporaryParcel
            }).ToListAsync();
            return Ok(datalist);
        }

        // GET: api/TemporarySectors/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemporarySector(int id)
        {
            var temporarySector = await _context.TemporarySector.Include(s=>s.TemporaryParcel).FirstOrDefaultAsync(s=>s.Id== id);
            
            if (temporarySector == null)
            {
                return NotFound();
            }
            TemporarySectorsDto data = new TemporarySectorsDto()
            {
                Id = temporarySector.Id,
                Name = temporarySector.Name,
                TemporaryParcelId = temporarySector.TemporaryParcel.Id
            };

            return Ok(data);
        }

        // PUT: api/TemporarySectors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporarySector(int id, [FromBody]TemporarySectorsDto temporarySector)
        {
            if (id != temporarySector.Id)
            {
                return BadRequest("melumatda xeta var");
            }
            var parcel = await _context.TemporaryParcel.FirstOrDefaultAsync(s => s.Id == temporarySector.TemporaryParcelId);

            if (parcel == null)
            {
                return BadRequest("bele bir sahe yoxdur");
            }
            var edited = await _context.TemporarySector.FirstOrDefaultAsync(s => s.Id == temporarySector.Id);
            edited.Name = temporarySector.Name;
            edited.TemporaryParcel = parcel;
            _context.Entry(edited).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporarySectorExists(id))
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

        // POST: api/TemporarySectors
        [HttpPost]
        public async Task<ActionResult> PostTemporarySector([FromBody]TemporarySectorsDto temporarySector)
        {
            var parcel = await _context.TemporaryParcel.FirstOrDefaultAsync(s => s.Id == temporarySector.TemporaryParcelId);

            if (parcel == null)
            {
                return BadRequest("bele bir sahe yoxdur");
            }
            TemporarySector sector = new TemporarySector()
            {
                Name = temporarySector.Name,
                TemporaryParcel = parcel,
                Status = true
            };
            _context.TemporarySector.Add(sector);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporarySectors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemporarySector(int id)
        {
            var temporarySector = await _context.TemporarySector.FindAsync(id);
            if (temporarySector == null)
            {
                return NotFound();
            }
            temporarySector.Status = false;
            _context.Entry(temporarySector).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TemporarySectorExists(int id)
        {
            return _context.TemporarySector.Any(e => e.Id == id);
        }
    }
}
