using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class UpdatePromotionDto
    {
        [StringLength(100, ErrorMessage = "Nome deve ter at� 100 caracteres.")]
        public string? Name { get; set; }

        [Range(0, 100, ErrorMessage = "Desconto deve ser entre 0 e 100.")]
        public decimal? DiscountPercentage { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "� necess�rio informar pelo menos um jogo.")]
        [MinLength(1, ErrorMessage = "� necess�rio informar pelo menos um jogo.")]
        public List<int> GameIds { get; set; } = [];
    }
}
