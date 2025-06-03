using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Nome � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "Nome deve ter at� 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Descri��o � obrigat�ria.")]
        [StringLength(500, ErrorMessage = "Descri��o deve ter at� 500 caracteres.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Pre�o � obrigat�rio.")]
        [Range(0, double.MaxValue, ErrorMessage = "Pre�o deve ser positivo.")]
        public decimal Price { get; set; }
    }
}
