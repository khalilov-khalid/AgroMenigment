using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionGroupDataController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;

        public PermissionGroupDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet]
        public async Task<ActionResult> GelModuls()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            var company = await _context.Companies.Include(s=>s.Packet).FirstOrDefaultAsync(s => s.Id == logineduser.Company.Id);

            List<int> ModulContent = JsonConvert.DeserializeObject<List<int>>(company.Packet.Content);

            return Ok(ModulContent);
        }
    }
}