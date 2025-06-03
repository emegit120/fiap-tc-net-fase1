namespace FIAPTechChallenge.Domain.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Muitos-para-muitos: uma promoção pode estar em vários jogos
        public ICollection<Game> Games { get; set; } = [];
    }
}
