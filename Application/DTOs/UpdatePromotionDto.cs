using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class UpdatePromotionDto
    {
        [StringLength(100, ErrorMessage = "Nome deve ter até 100 caracteres.")]
        public string? Name { get; set; }

        [Range(0, 100, ErrorMessage = "Desconto deve ser entre 0 e 100.")]
        public decimal? DiscountPercentage { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "É necessário informar pelo menos um jogo.")]
        [MinLength(1, ErrorMessage = "É necessário informar pelo menos um jogo.")]
        public List<int> GameIds { get; set; } = [];
    }
}
