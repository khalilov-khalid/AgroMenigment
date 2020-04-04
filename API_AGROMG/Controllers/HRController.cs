using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API_AGROMG.Model;
using Newtonsoft.Json;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HRController : ControllerBase
    {
        private readonly IHRRepository _repo;

        private readonly ILanguageRepository _lang;

        private readonly IAuthRepository _auth;

        public HRController(IHRRepository repo, ILanguageRepository lang, IAuthRepository auth)
        {
            _lang = lang;
            _repo = repo;
            _auth = auth;
        }


        [HttpGet]
        public async Task<ActionResult> GelAllUsers()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (await _auth.VerifyUser(id) == null)
            {
                return Unauthorized();
            }

            var users = await _repo.GetUsers(id);

            List<UserDataForHR> userlist = users.Select(s => new UserDataForHR()
            {
                Id = s.Id,
                Name = s.Name,
                Adress = s.Adress,
                Birthday = s.Birthday,
                Email=s.Email,
                Tel=s.Tel
            }).ToList();

            return Ok(userlist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id) 
        {
            var user = await _repo.GetUser(id);

            if (user==null)
            {
                return NotFound();
            }
            UserForEditDtos compamyUsers = new UserForEditDtos()
            {
                ID = user.Id,
                Name = user.Name,
                AdminStatus = user.AdminStatus,
                Adress = user.Adress,
                Birthday = user.Birthday,
                Salary = user.Salary,
                Gender = user.Gender,
                Email = user.Email,
                Phone = user.Tel
            };
            compamyUsers.ProfessionID = await _repo.GetUserProfessions(compamyUsers.ID);

            return Ok(compamyUsers);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditUser(int id , [FromBody]UserForEditDtos user)
        {
            if (id != user.ID)
            {
                return BadRequest("Datada Sehvlik var");
            }
            var editeduser = await _repo.GetUser(user.ID);

            editeduser.Name = user.Name;
            editeduser.AdminStatus = user.AdminStatus;
            editeduser.Birthday = user.Birthday;
            editeduser.Salary = user.Salary;
            editeduser.Email = user.Email;
            editeduser.Tel = user.Phone;
            editeduser.Adress = user.Adress;
            editeduser.Gender = user.Gender;

            var StatusAction = await _repo.UpdateUser(editeduser, user.PermissionGroupId, user.ProfessionID);

            if (!StatusAction)
            {
                return BadRequest("Emeliyyat ugursuz");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {            
            var StatusAction = await _repo.DeleteUser(id);
            if (!StatusAction)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody]UserForAddDtos user)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!await _repo.CheckHumanCount(id))
            {
                return BadRequest("Paketinizin istifadə limitini aşmisiz");
            }
            var logineduser = await _auth.VerifyUser(id);

            if (await _auth.UserExists(user.Username)) return BadRequest("Username already exists");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            User new_user = new User()
            {
                Name=user.Name,
                Username=user.Username,
                PasswordHash= passwordHash,
                PasswordSalt=passwordSalt,
                AdminStatus = user.AdminStatus,
                Status =true,
                Birthday =user.Birthday,
                Salary = user.Salary,
                Email =user.Email,
                Tel = user.Phone,
                Adress = user.Adress,
                Gender = user.Gender,
                Company = logineduser.Company
            };
            var createStatus = await _repo.AddUser(new_user, user.PermissionGroupId, user.ProfessionID);

            if (!createStatus)
            {
                return BadRequest();
            }

            return StatusCode(201);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}