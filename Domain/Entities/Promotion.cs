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

        protected Promotion() { }

        public Promotion(string name, decimal discountPercentage, DateTime startDate, DateTime endDate)
        {
            SetName(name);
            SetDiscountPercentage(discountPercentage);
            StartDate = startDate;
            EndDate = endDate;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório.");
            Name = name;
        }


        public void SetDiscountPercentage(decimal discountPercentage)
        {
            if (discountPercentage <= 0)
                throw new ArgumentException("O desconto deve ser maior que 0");
            DiscountPercentage = discountPercentage;
        }

    }
}
