namespace FIAPTechChallenge.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Category> Categories { get; set; } = new List<Category>();


        protected Game() { }

        public Game(string name, string description, decimal price)
        {
            SetName(name);
            SetDescription(description);
            SetPrice(price);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do jogo é obrigatório.");
            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("A descrição do jogo é obrigatória.");
            Description = description;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("O preço do jogo deve ser 0 ou positivo.");
            Price = price;
        }
    }
}
