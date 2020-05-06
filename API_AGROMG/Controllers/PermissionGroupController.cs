using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
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
    public class PermissionGroupController : ControllerBase
    {

        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public PermissionGroupController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        } 

        [HttpPost]
        public async Task<ActionResult> AddPermissionGroup([FromBody] PermissionGroupForAddDto permission)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var loginedUser = await _auth.VerifyUser(id);

            PermissionsGroups new_group = new PermissionsGroups()
            {
                Name = permission.Name,
                RolContent = JsonConvert.SerializeObject(permission.Permissions.OrderBy(s=>s.ModulKey)),
                Status = true,
                Company = loginedUser.Company
            };

            await _context.PermissionsGroups.AddAsync(new_group);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult> GetGroups()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var loginedUser = await _auth.VerifyUser(id);

            var grouplist = await _context.PermissionsGroups.Where(s => s.Company.Id == loginedUser.Company.Id && s.Status==true).ToListAsync();

            List<PermissionGroupForReadandUpdateDto> permissions = grouplist.Select(s => new PermissionGroupForReadandUpdateDto()
            {
                Id = s.Id,
                Name = s.Name,
                Permissions = JsonConvert.DeserializeObject<List<PermissionDto>>(s.RolContent)
            }).ToList();

            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetGroup(int id)
        {
            var selectedGroup = await _context.PermissionsGroups.FirstOrDefaultAsync(p => p.Id == id);

            PermissionGroupForReadandUpdateDto group = new PermissionGroupForReadandUpdateDto()
            {
                Id = selectedGroup.Id,
                Name = selectedGroup.Name,
                Permissions = JsonConvert.DeserializeObject<List<PermissionDto>>(selectedGroup.RolContent)
            };

            return Ok(group);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateGroup(int id, [FromBody]PermissionGroupForReadandUpdateDto group)
        {
            var findedGroup = await _context.PermissionsGroups.FirstOrDefaultAsync(p => p.Id == id);

            if (findedGroup == null)
            {
                return BadRequest("Tapilmadi");
            }

            findedGroup.Name = group.Name;
            findedGroup.RolContent = JsonConvert.SerializeObject(group.Permissions);

            _context.Entry(findedGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var deletedGroup = await _context.PermissionsGroups.FirstOrDefaultAsync(s => s.Id == id);

            var userGroups = await _context.Users.Where(s => s.PermissionsGroups == deletedGroup).ToListAsync();

            if (userGroups.Count == 0)
            {
                deletedGroup.Status = false;
                _context.Entry(deletedGroup).State = EntityState.Modified;
                await _context.SaveChangesAsync(); 
            }
            else
            {
                return BadRequest("Bu qrupa aid olan istifadeciler var. Bu Grup siline bilmez");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("defaultPer")]
        public async Task<ActionResult> GetDefaultGroup()
        {
            var selectedGroup = await _context.PermissionsGroups.FirstOrDefaultAsync(p => p.Company == null);

            PermissionGroupForReadandUpdateDto group = new PermissionGroupForReadandUpdateDto()
            {
                Id = selectedGroup.Id,
                Name = selectedGroup.Name,
                Permissions = JsonConvert.DeserializeObject<List<PermissionDto>>(selectedGroup.RolContent)
            };

            return Ok(group);
        }

        [AllowAnonymous]
        [HttpPut("defaultPer/{Id}")]
        public async Task<ActionResult> defaultUpdate(int id, [FromBody]PermissionGroupForReadandUpdateDto group)
        {
            var findedGroup = await _context.PermissionsGroups.FirstOrDefaultAsync(p => p.Id == id);

            if (findedGroup == null)
            {
                return BadRequest("Tapilmadi");
            }

            findedGroup.Name = group.Name;
            findedGroup.RolContent = JsonConvert.SerializeObject(group.Permissions);

            _context.Entry(findedGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}