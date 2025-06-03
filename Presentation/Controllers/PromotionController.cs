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
    public class PromotionController(
        IRepository<Promotion> repository,
        FiapDbContext context,
        ILogger<PromotionController> logger) : ControllerBase
    {
        private readonly IRepository<Promotion> _repository = repository;
        private readonly FiapDbContext _context = context;
        private readonly ILogger<PromotionController> _logger = logger;

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionResponseDto>>> GetAll()
        {
            _logger.LogInformation("Listando todas as promo��es.");
            var promotions = await _context.Promotions
                .Include(p => p.Games)
                .ToListAsync();

            var result = promotions.Select(p => new PromotionResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                DiscountPercentage = p.DiscountPercentage,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                GameIds = p.Games.Select(g => g.Id).ToList()
            });
            return Ok(result);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionResponseDto>> GetById(int id)
        {
            _logger.LogInformation("Buscando promo��o por ID: {PromotionId}", id);
            var promotion = await _context.Promotions
                .Include(p => p.Games)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (promotion == null)
            {
                _logger.LogWarning("Promo��o n�o encontrada: {PromotionId}", id);
                return NotFound();
            }

            var result = new PromotionResponseDto
            {
                Id = promotion.Id,
                Name = promotion.Name,
                DiscountPercentage = promotion.DiscountPercentage,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                GameIds = promotion.Games.Select(g => g.Id).ToList()
            };
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova promo��o (Acesso: Admin)")]
        public async Task<ActionResult<PromotionResponseDto>> Create([FromBody] CreatePromotionDto dto)
        {
            _logger.LogInformation("Tentando criar promo��o: {Name}", dto.Name);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState inv�lido ao criar promo��o.");
                return BadRequest(ModelState);
            }

            var games = await _context.Games
               .Where(g => dto.GameIds.Contains(g.Id))
               .ToListAsync();

            if (games.Count != dto.GameIds.Count)
            {
                _logger.LogWarning("Um ou mais jogos informados n�o existem ao criar promo��o.");
                return BadRequest(new { error = "Um ou mais jogos informados n�o existem." });
            }

            var promotion = new Promotion(dto.Name, dto.DiscountPercentage, dto.StartDate, dto.EndDate);

            try
            {
                await _repository.AddAsync(promotion);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Promo��o criada com sucesso: {PromotionId}", promotion.Id);

                var result = new PromotionResponseDto
                {
                    Id = promotion.Id,
                    Name = promotion.Name,
                    DiscountPercentage = promotion.DiscountPercentage,
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate,
                    GameIds = promotion.Games.Select(g => g.Id).ToList()
                };

                return CreatedAtAction(nameof(GetById), new { id = promotion.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar promo��o.");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma promo��o existente (Acesso: Admin)")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePromotionDto dto)
        {
            _logger.LogInformation("Atualizando promo��o: {PromotionId}", id);

            var promotion = await _context.Promotions
                .Include(p => p.Games)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (promotion == null)
            {
                _logger.LogWarning("Promo��o n�o encontrada para atualiza��o: {PromotionId}", id);
                return NotFound();
            }

            try
            {
                if (dto.Name is not null)
                    promotion.Name = dto.Name;

                if (dto.DiscountPercentage is not null)
                    promotion.DiscountPercentage = dto.DiscountPercentage.Value;

                if (dto.StartDate is not null)
                    promotion.StartDate = dto.StartDate.Value;

                if (dto.EndDate is not null)
                    promotion.EndDate = dto.EndDate.Value;

                if (dto.GameIds is not null)
                {
                    var games = await _context.Games
                        .Where(g => dto.GameIds.Contains(g.Id))
                        .ToListAsync();

                    if (games.Count != dto.GameIds.Count)
                    {
                        _logger.LogWarning("Um ou mais jogos informados n�o existem ao atualizar promo��o.");
                        return BadRequest(new { error = "Um ou mais jogos informados n�o existem." });
                    }

                    promotion.Games.Clear();
                    foreach (var game in games)
                        promotion.Games.Add(game);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation("Promo��o atualizada com sucesso: {PromotionId}", promotion.Id);

                var result = new PromotionResponseDto
                {
                    Id = promotion.Id,
                    Name = promotion.Name,
                    DiscountPercentage = promotion.DiscountPercentage,
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate,
                    GameIds = promotion.Games.Select(g => g.Id).ToList()
                };

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de valida��o ao atualizar promo��o: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar promo��o.");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove uma promo��o (Acesso: Admin)")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Removendo promo��o: {PromotionId}", id);

            var promotion = await _repository.GetByIdAsync(id);
            if (promotion == null)
            {
                _logger.LogWarning("Promo��o n�o encontrada para remo��o: {PromotionId}", id);
                return NotFound();
            }

            try
            {
                _repository.Remove(promotion);
                await _repository.SaveChangesAsync();
                _logger.LogInformation("Promo��o removida com sucesso: {PromotionId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover promo��o.");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
