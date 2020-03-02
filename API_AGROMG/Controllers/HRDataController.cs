using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.SimpleforDtos;
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
    public class HRDataController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;

        
        public HRDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet("professions/{lang}")]
        public async Task<ActionResult> GetProdessionsAndGender(string lang)
        {
            var professions = await _context.Professions.Where(s => s.Status == true).ToListAsync();


            List<StandartDto> professionList = professions.Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = _context.LanguageContexts.FirstOrDefault(l => l.Key == s.Key && l.LangUnicode == lang).Context
            }).ToList();
            
            return Ok(professionList);
        }

        [HttpGet("PermissionGroup")]
        public async Task<ActionResult> GetPermissionGroup()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            List<StandartDto> PermissionList = await _context.PermissionsGroups.Where(w=>w.Company==logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name =s.Name

            }).ToListAsync();

            return Ok(PermissionList);
        }
    }    
}