using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        private class AdminUser
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Admin(int id)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync();

            if (admin == null)
            {
                return NotFound();
            }

            AdminUser loginedadmin = new AdminUser()
            {
                Id = 1,
                Name = admin.Username
            };
            
            return Ok(admin);
        }

    }
}