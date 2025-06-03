using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class CreatePromotionDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome deve ter até 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Desconto deve ser entre 0 e 100.")]
        public decimal DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Data de início é obrigatória.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Data de término é obrigatória.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "É necessário informar pelo menos um jogo.")]
        [MinLength(1, ErrorMessage = "É necessário informar pelo menos um jogo.")]
        public List<int> GameIds { get; set; } = [];

    }
}
