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
using Microsoft.EntityFrameworkCore;
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

        private readonly DataContext _context;        

        public AuthController(DataContext context,IAuthRepository repo, IConfiguration config)
        {
            _context = context;
            _config = config;
            _repo = repo;
        }

        [HttpPost("adminlogin")]
        public async Task<IActionResult> AdminLogin(UserForLoginDto userForLoginDto)
        {
            var userFormRepo = await _context.Admins.FirstOrDefaultAsync(x => x.Username == userForLoginDto.Username.ToLower() && x.Password == userForLoginDto.Password);

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

            Company New_Company = new Company()
            {
                Name = userForRegisterDto.CompanyName,
                Address = userForRegisterDto.CompanyAdress,
                Email = userForRegisterDto.CompanyEmail,
                Tel = JsonConvert.SerializeObject(userForRegisterDto.CompanyTel),
                HumanCount = 1,
                Status = true,
                StatusFinishDate = userForRegisterDto.PaymentEndDate,
                Packet = _context.Packets.Where(s => s.Id == userForRegisterDto.PacketId).FirstOrDefault()
            };
            await _context.Companies.AddAsync(New_Company);
            await _context.SaveChangesAsync();


            Workers New_Worker = new Workers()
            {
                Name = userForRegisterDto.UserName,
                Adress = userForRegisterDto.UserAdress,
                Birthday = userForRegisterDto.UserBirthday,
                Status = true,
                Email = userForRegisterDto.UserEmail,
                Tel = userForRegisterDto.UserTel,
                Gender = userForRegisterDto.UserGender,
                Company = New_Company
            };
            await _context.Workers.AddAsync(New_Worker);
            await _context.SaveChangesAsync();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRegisterDto.UserPassword, out passwordHash, out passwordSalt);
            Users new_user = new Users()
            {
                Workers = New_Worker,
                Username = userForRegisterDto.UserUsername,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PermissionsGroups = await _context.PermissionsGroups.FirstOrDefaultAsync(s => s.Company == null),
                Status =true                
            };

            await _context.Users.AddAsync(new_user);
            await _context.SaveChangesAsync();


            var createdUser = new_user;


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
            return Ok(new { token = tokenHandler.WriteToken(token) });
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
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _repo.Logineduser(id);

            if (logineduser == null)
            {
                return NotFound();
            }

            return Ok(logineduser);
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