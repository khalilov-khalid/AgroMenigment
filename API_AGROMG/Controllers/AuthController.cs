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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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

        [HttpPost("logout")]

        public ActionResult Logout()
        {
            return StatusCode(200);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegisterDto)
        {
            // validete reguest

            userForRegisterDto.UserUsername = userForRegisterDto.UserUsername.ToLower();

            if (await _repo.UserExists(userForRegisterDto.UserUsername)) return BadRequest("Username already exists");

            var companyToCreate = new Company
            {
                Name = userForRegisterDto.CompanyName,
                Address=userForRegisterDto.CompanyAdress,
                Email= userForRegisterDto.CompanyEmail,
                Tel= JsonConvert.SerializeObject(userForRegisterDto.CompanyTel),
                HumanCount=1,
                Status=true,
                StatusFinishDate= userForRegisterDto.PaymentEndDate,                
            };

            var userToCreate = new User
            {
                Name=userForRegisterDto.UserName,
                Username = userForRegisterDto.UserUsername,
                Adress= userForRegisterDto.UserAdress,
                Birthday = userForRegisterDto.UserBirthday,
                Status=true,
                AdminStatus=true,
                Salary=0,
                Email = userForRegisterDto.UserEmail,
                Tel =userForRegisterDto.UserTel
            };

            var createdUser = await _repo.Register(companyToCreate, userToCreate, userForRegisterDto.UserPassword, userForRegisterDto.PacketId, userForRegisterDto.UserGenderID, userForRegisterDto.UserProfessionID);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString()),
                new Claim(ClaimTypes.Name, createdUser.Username)
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
            return Ok(new { token = tokenHandler.WriteToken(token)});
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFormRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

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
            return Ok(new { token = tokenHandler.WriteToken(token)});
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _repo.Logineduser(id);
            if (logineduser==null)
            {
                return NotFound();
            }            
            return Ok(logineduser);
        }

        
    }
    
}