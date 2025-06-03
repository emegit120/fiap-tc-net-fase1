using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class UpdateGameDto
    {
        [StringLength(100, ErrorMessage = "Nome deve ter até 100 caracteres.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Descrição deve ter até 500 caracteres.")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Preço deve ser positivo.")]
        public decimal? Price { get; set; }

    }
}
