using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public UserController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAccount([FromBody] UserCreateDto user)
        {
            user.Username = user.Username.ToLower();
            if (await _auth.UserExists(user.Username)) return BadRequest("Username already exists");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            Users new_user = new Users()
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Workers = await _context.Workers.FirstOrDefaultAsync(s => s.Id == user.WorkerId),
                PermissionsGroups = await _context.PermissionsGroups.FirstOrDefaultAsync(s => s.Id == user.PermissionGroupId),
                Status = true
            };

            await _context.Users.AddAsync(new_user);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult> Accounts()
        {
            List<StandartDto> userList = await _context.Users.Where(s => s.Status == true).Select(s => new StandartDto()
            {
                Id = s.Workers.Id,
                Name = s.Workers.Name
            }).ToListAsync();
            return Ok(userList);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAccount(int id)
        {
            var deletedAccount = await _context.Users.FirstOrDefaultAsync(s=>s.Id == id);
            if (deletedAccount == null) return BadRequest("Bele bir melumat yoxdur");

            deletedAccount.Status = false;
            _context.Entry(deletedAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ResertAccountPassword(int id ,[FromBody] UserResetDto userPassword)
        {
            if (id!= userPassword.Id)
            {
                return BadRequest("Melumatin gonderilmesinde xeta var");
            }
            var user = await _context.Users.FirstOrDefaultAsync(s=>s.Workers.Id == userPassword.Id);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userPassword.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
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