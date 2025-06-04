using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_sbd.Data;
using Projekt_sbd.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ✅ REJESTRACJA – tylko dla ADMINA
        [HttpPost("register")]
        [AllowAnonymous] // ← to pozwoli każdemu zarejestrować się (np. admina na początku)
        public IActionResult Register([FromBody] User user)
        {
            bool istnieje = _context.Users.Count(u => u.Username == user.Username) > 0;
            if (istnieje)
                return BadRequest("Użytkownik już istnieje");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Zarejestrowano pomyślnie");
        }

        // 🔓 LOGOWANIE – dostępne dla każdego
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash))
                return Unauthorized("Niepoprawne dane logowania");

            // 📌 Logowanie do logów
            var log = new LoginLog
            {
                Username = user.Username,
                LoginTime = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"
            };
            _context.LoginLogs.Add(log);
            _context.SaveChanges();

            var token = GenerateJwt(user);
            return Ok(new { token });
        }

        // 🔐 GENEROWANIE JWT
        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // 👈 ID użytkownika
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
