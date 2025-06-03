using FIAPTechChallenge.Application.DTOs;
using FIAPTechChallenge.Domain.Interfaces;
using FIAPTechChallenge.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace FIAPTechChallenge.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        IUserRepository userRepository,
        IConfiguration configuration,
        ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState inválido ao tentar login para o e-mail: {Email}", dto.Email);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Tentativa de login para o e-mail: {Email}", dto.Email);

                var user = await _userRepository.GetByEmailAsync(dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                {
                    _logger.LogWarning("Falha de autenticação para o e-mail: {Email}", dto.Email);
                    return BadRequest(new { error = "E-mail ou senha inválidos." });
                }

                var token = GenerateJwtToken(user);
                _logger.LogInformation("Login realizado com sucesso para o e-mail: {Email}", dto.Email);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao realizar login para o e-mail: {Email}", dto.Email);
                return StatusCode(500, new { error = "Erro inesperado ao realizar login.", details = ex.Message });
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("roleId", user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
