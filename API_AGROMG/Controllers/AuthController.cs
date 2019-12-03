using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API_AGROMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegisterDto)
        {
            // validete reguest

            userForRegisterDto.UserUsername = userForRegisterDto.UserUsername.ToLower();

            if (await _repo.UserExists(userForRegisterDto.UserUsername)) return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = userForRegisterDto.UserUsername
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.UserPassword);

            return StatusCode(201);
        }

        [HttpPost("adminlogin")]
        public async Task<IActionResult> AdminLogin(UserForLoginDto userForLoginDto)
        {
            var userFormRepo = await _repo.AdminLogin(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFormRepo == null) return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFormRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFormRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });

        }
    }

   
}