namespace FIAPTechChallenge.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Relacionamento muitos-para-muitos com Game
        public ICollection<Game> Games { get; set; } = [];
    }
}
