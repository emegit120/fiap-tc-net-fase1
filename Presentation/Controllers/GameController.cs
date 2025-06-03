using FIAPTechChallenge.Application.DTOs;
using FIAPTechChallenge.Domain.Entities;
using FIAPTechChallenge.Domain.Interfaces;
using FIAPTechChallenge.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAPTechChallenge.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GameController(
        IRepository<Game> repository,
        FiapDbContext context,
        ILogger<GameController> logger) : ControllerBase
    {
        private readonly IRepository<Game> _repository = repository;
        private readonly FiapDbContext _context = context;
        private readonly ILogger<GameController> _logger = logger;

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameResponseDto>>> GetAll()
        {
            _logger.LogInformation("Listando todos os jogos.");
            var games = await _repository.GetAllAsync();

            var now = DateTime.UtcNow;

            var promotions = await _context.Promotions
                .Include(p => p.Games)
                .Where(p => p.StartDate <= now && p.EndDate >= now)
                .ToListAsync();

            var result = games.Select(g =>
            {
                var gamePromotions = promotions
                    .Where(p => p.Games.Any(game => game.Id == g.Id) && p.DiscountPercentage > 0)
                    .OrderByDescending(p => p.DiscountPercentage)
                    .ToList();

                decimal? discountedPrice = null;
                if (gamePromotions.Any())
                {
                    var bestPromotion = gamePromotions.First();
                    discountedPrice = g.Price * (1 - (bestPromotion.DiscountPercentage / 100m));
                    if (discountedPrice < 0)
                        discountedPrice = 0;
                }

                return new GameResponseDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    Price = g.Price,
                    DiscountedPrice = discountedPrice,
                    CreatedAt = g.CreatedAt,
                    UpdatedAt = g.UpdatedAt
                };
            });
            return Ok(result);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GameResponseDto>> GetById(int id)
        {
            _logger.LogInformation("Buscando jogo por ID: {GameId}", id);
            var game = await _repository.GetByIdAsync(id);
            if (game == null)
            {
                _logger.LogWarning("Jogo não encontrado: {GameId}", id);
                return NotFound();
            }

            var now = DateTime.UtcNow;

            var promotions = await _context.Promotions
                .Include(p => p.Games)
                .Where(p => p.StartDate <= now && p.EndDate >= now && p.Games.Any(gm => gm.Id == id) && p.DiscountPercentage > 0)
                .OrderByDescending(p => p.DiscountPercentage)
                .ToListAsync();

            decimal? discountedPrice = null;
            if (promotions.Any())
            {
                var bestPromotion = promotions.First();
                discountedPrice = game.Price * (1 - (bestPromotion.DiscountPercentage / 100m));
                if (discountedPrice < 0)
                    discountedPrice = 0;
            }

            var result = new GameResponseDto
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                DiscountedPrice = discountedPrice,
                CreatedAt = game.CreatedAt,
                UpdatedAt = game.UpdatedAt
            };
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo jogo (Acesso: Admin)")]
        public async Task<ActionResult<GameResponseDto>> Create([FromBody] CreateGameDto dto)
        {
            _logger.LogInformation("Tentando criar jogo: {Name}", dto.Name);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState inválido ao criar jogo.");
                return BadRequest(ModelState);
            }

            var game = new Game(dto.Name, dto.Description, dto.Price);

            try
            {
                await _repository.AddAsync(game);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Jogo criado com sucesso: {GameId}", game.Id);

                var result = new GameResponseDto
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    CreatedAt = game.CreatedAt,
                    UpdatedAt = game.UpdatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = game.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar jogo.");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um jogo existente (Acesso: Admin)")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGameDto dto)
        {
            _logger.LogInformation("Atualizando jogo: {GameId}", id);

            var game = await _repository.GetByIdAsync(id);
            if (game == null)
            {
                _logger.LogWarning("Jogo não encontrado para atualização: {GameId}", id);
                return NotFound();
            }

            try
            {
                if (dto.Name is not null)
                    game.SetName(dto.Name);

                if (dto.Description is not null)
                    game.SetDescription(dto.Description);

                if (dto.Price is not null)
                    game.SetPrice(dto.Price.Value);

                await _repository.SaveChangesAsync();

                _logger.LogInformation("Jogo atualizado com sucesso: {GameId}", game.Id);

                var result = new GameResponseDto
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    CreatedAt = game.CreatedAt,
                    UpdatedAt = game.UpdatedAt
                };

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar jogo: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar jogo.");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove um jogo (Acesso: Admin)")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Removendo jogo: {GameId}", id);

            var game = await _repository.GetByIdAsync(id);
            if (game == null)
            {
                _logger.LogWarning("Jogo não encontrado para remoção: {GameId}", id);
                return NotFound();
            }

            try
            {
                _repository.Remove(game);
                await _repository.SaveChangesAsync();
                _logger.LogInformation("Jogo removido com sucesso: {GameId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover jogo.");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
