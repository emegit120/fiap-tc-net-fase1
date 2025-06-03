using System.Text.RegularExpressions;

namespace FIAPTechChallenge.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Game> Games { get; set; } = [];
        

        protected User() { }

        public User(string name, string email, string password, int roleId)
        {
            SetName(name);
            SetEmail(email);
            SetPassword(password);
            RoleId = roleId;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório.");
            Name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("E-mail é obrigatório.");

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
                throw new ArgumentException("E-mail inválido.");

            Email = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Senha é obrigatória.");
            if (password.Length < 8 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter) ||
                !password.Any(ch => !char.IsLetterOrDigit(ch)))
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres, letras, números e caractere especial.");
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
