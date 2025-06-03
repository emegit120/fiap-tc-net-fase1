using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class UpdateUserDto
    {
        [StringLength(100, ErrorMessage = "Nome deve ter até 100 caracteres.")]
        public string? Name { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Senha deve ter pelo menos 8 caracteres.")]
        public string? Password { get; set; }

        public List<int> GameIds { get; set; } = [];

    }
}
