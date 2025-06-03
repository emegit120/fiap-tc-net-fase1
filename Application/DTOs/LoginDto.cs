using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class LoginDto
    {
        [DefaultValue("email@email.com")]
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = null!;

        [DefaultValue("senha")]
        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string Password { get; set; } = null!;
    }
}
