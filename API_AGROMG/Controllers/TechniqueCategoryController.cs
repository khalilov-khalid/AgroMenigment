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
    public class TechniqueCategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public TechniqueCategoryController(DataContext context)
        {
            _context = context;
        }

        
        //[HttpPost]
        //public async Task<ActionResult> AddCategory()
        //{


        //    return StatusCode(201);
        //}
    }
}