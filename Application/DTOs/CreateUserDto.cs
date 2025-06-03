using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome deve ter até 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Senha deve ter pelo menos 8 caracteres.")]
        public string Password { get; set; } = null!;

    }
}
