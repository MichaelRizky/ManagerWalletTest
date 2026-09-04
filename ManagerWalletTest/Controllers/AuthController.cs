using BCrypt.Net;
using ManagerWalletTest.Data;
using ManagerWalletTest.DTOs;
using ManagerWalletTest.Helpers;
using ManagerWalletTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagerWalletTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                return BadRequest("Password and Confirm Password do not match.");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("Email already exists.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            // Berikan dompet kosong saat registrasi
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Balance = 0
            };

            _context.Users.Add(user);
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigHelper.GetJwtSecret());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Logout otomatis 15 menit
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

        [Authorize]
        [HttpGet("protected-view")]
        public IActionResult ProtectedView()
        {
            // Menarik email langsung dari token JWT yang divalidasi
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok($"Hello {email}, welcome back");
        }
    }
}