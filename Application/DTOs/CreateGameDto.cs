using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome deve ter até 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "Descrição deve ter até 500 caracteres.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Preço é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "Preço deve ser positivo.")]
        public decimal Price { get; set; }
    }
}
