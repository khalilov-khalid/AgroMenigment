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
    public class TemporaryPayAccountsController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporaryPayAccountsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TemporaryPayAccounts
        [HttpGet]
        public async Task<ActionResult> GetTemporaryPayAccount()
        {
            List<StandartDto> datalist = await _context.TemporaryPayAccount.Where(s => s.Status == true).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            return Ok(datalist);
        }

        // GET: api/TemporaryPayAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemporaryPayAccount(int id)
        {
            var temporaryPayAccount = await _context.TemporaryPayAccount.FindAsync(id);

            if (temporaryPayAccount == null)
            {
                return NotFound();
            }
            StandartDto data = new StandartDto()
            {
                Id = temporaryPayAccount.Id,
                Name = temporaryPayAccount.Name
            };

            return Ok(data);
        }

        // PUT: api/TemporaryPayAccounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporaryPayAccount(int id, TemporaryPayAccount temporaryPayAccount)
        {
            if (id != temporaryPayAccount.Id)
            {
                return BadRequest();
            }
            temporaryPayAccount.Status = true;
            _context.Entry(temporaryPayAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporaryPayAccountExists(id))
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

        // POST: api/TemporaryPayAccounts
        [HttpPost]
        public async Task<ActionResult> PostTemporaryPayAccount(TemporaryPayAccount temporaryPayAccount)
        {
            temporaryPayAccount.Status = true;
            _context.TemporaryPayAccount.Add(temporaryPayAccount);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/TemporaryPayAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemporaryPayAccount(int id)
        {
            var temporaryPayAccount = await _context.TemporaryPayAccount.FindAsync(id);
            if (temporaryPayAccount == null)
            {
                return NotFound();
            }
            temporaryPayAccount.Status = false;
            _context.Entry(temporaryPayAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TemporaryPayAccountExists(int id)
        {
            return _context.TemporaryPayAccount.Any(e => e.Id == id);
        }
    }
}
