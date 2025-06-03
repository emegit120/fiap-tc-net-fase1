using FIAPTechChallenge.Application.DTOs;
using FIAPTechChallenge.Domain.Entities;
using FIAPTechChallenge.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAPTechChallenge.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IUserRepository repository, IRepository<Role> roleRepository, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserRepository _repository = repository;
        private readonly IRepository<Role> _roleRepository = roleRepository;
        private readonly ILogger<UserController> _logger = logger;

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os usuários (Acesso: Admin)")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            _logger.LogInformation("Listando todos os usuários.");
            var users = await _repository.GetAllAsync();
            var result = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            });
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém um usuário por ID (Acesso: Admin)")]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            _logger.LogInformation("Buscando usuário por ID: {UserId}", id);
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado: {UserId}", id);
                return NotFound();
            }

            var result = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserDto dto)
        {
            try
            {
                _logger.LogInformation("Tentando criar usuário com e-mail: {Email}", dto.Email);
                var user = new User(dto.Name, dto.Email, dto.Password, 1);

                var existingUser = await _repository.GetByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning("E-mail já cadastrado: {Email}", user.Email);
                    return BadRequest(new { error = "E-mail já cadastrado. Informe outro e-mail." });
                }

                var role = await _roleRepository.GetByIdAsync(user.RoleId);
                if (role == null)
                {
                    _logger.LogWarning("RoleId inválido: {RoleId}", user.RoleId);
                    return BadRequest(new { error = "RoleId inválido. Informe um valor existente." });
                }

                await _repository.AddAsync(user);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Usuário criado com sucesso: {UserId}", user.Id);

                var result = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = user.Id }, result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao criar usuário: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar usuário.");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um usuário (Acesso: Admin)")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            _logger.LogInformation("Atualizando usuário: {UserId}", id);
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado para atualização: {UserId}", id);
                return NotFound();
            }

            try
            {
                if (dto.Name is not null)
                    user.SetName(dto.Name);

                if (dto.Email is not null)
                    user.SetEmail(dto.Email);

                if (dto.Password is not null)
                    user.SetPassword(dto.Password);

                await _repository.SaveChangesAsync();

                _logger.LogInformation("Usuário atualizado com sucesso: {UserId}", user.Id);

                var result = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar usuário: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar usuário.");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove um usuário (Acesso: Admin)")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Removendo usuário: {UserId}", id);
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado para remoção: {UserId}", id);
                return NotFound();
            }

            try
            {
                _repository.Remove(user);
                await _repository.SaveChangesAsync();
                _logger.LogInformation("Usuário removido com sucesso: {UserId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover usuário.");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
