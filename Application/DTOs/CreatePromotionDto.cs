using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class CreatePromotionDto
    {
        [Required(ErrorMessage = "Nome � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "Nome deve ter at� 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Desconto deve ser entre 0 e 100.")]
        public decimal DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Data de in�cio � obrigat�ria.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Data de t�rmino � obrigat�ria.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "� necess�rio informar pelo menos um jogo.")]
        [MinLength(1, ErrorMessage = "� necess�rio informar pelo menos um jogo.")]
        public List<int> GameIds { get; set; } = [];

    }
}
