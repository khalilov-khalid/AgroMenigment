using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            AdminUser admin = new AdminUser()
            {
                Id = 1,
                Name = "Rooter"
            };
            
            return Ok(admin);
        }

    }
}