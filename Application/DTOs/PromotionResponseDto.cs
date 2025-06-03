using System.ComponentModel.DataAnnotations;

namespace FIAPTechChallenge.Application.DTOs
{
    public class PromotionResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> GameIds { get; set; } = [];
    }
}
