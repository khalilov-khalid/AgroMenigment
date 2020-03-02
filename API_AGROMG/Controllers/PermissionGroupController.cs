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
using Newtonsoft.Json;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionGroupController : ControllerBase
    {
        private readonly IPremissionGroupRepository _repo;

        private readonly IAuthRepository _auth;
        public PermissionGroupController(IPremissionGroupRepository repo, IAuthRepository auth)
        {
            _repo = repo;
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

            var createdGroup = await _repo.AddPermission(new_group);

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult> GetGroups()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var loginedUser = await _auth.VerifyUser(id);

            var grouplist = await _repo.GetAllGroups(loginedUser.Company.Id);

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
            var selectedGroup = await _repo.GetGroup(id);

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
            var findedGroup = await _repo.GetGroup(id);

            if (findedGroup == null)
            {
                return BadRequest("Tapilmadi");
            }

            findedGroup.Name = group.Name;
            findedGroup.RolContent = JsonConvert.SerializeObject(group.Permissions);

            var editedGroup = await _repo.UpdatePermission(findedGroup);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var status = await _repo.DeletePermission(id);

            if (!status)
            {
                return BadRequest("Bu qrupa aid olan istifadeciler var. Bu Grup siline bilmez");
            }

            return Ok();
        }
    }
}