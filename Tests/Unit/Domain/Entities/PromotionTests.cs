using FIAPTechChallenge.Domain.Entities;
using System;
using Xunit;

namespace FiapTechChallenge.Tests.Domain.Entities
{
    public class PromotionTests
    {
        [Fact]
        public void CreatePromotion_ValidData_ShouldSetProperties()
        {
            var promotion = new Promotion
            (
                "Promoção",
                20,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                []
            );

            Assert.Equal("Promoção", promotion.Name);
            Assert.Equal(20, promotion.DiscountPercentage);
            Assert.True(promotion.EndDate > promotion.StartDate);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(50.5)]
        public void DiscountPercentage_ValidValues_ShouldSet(decimal discount)
        {
            var promotion = new Promotion
            (
                "Promoção",
                discount,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                []
            );

            Assert.True(promotion.DiscountPercentage > 0);
            
        }


        [Fact]
        public void EndDate_Before_StartDate_ShouldBeInvalid()
        {
            var start = DateTime.UtcNow;
            var end = start.AddDays(-1);

            var promotion = new Promotion
            (
                "Promoção",
                10,
                start,
                end,
                []
            );

            Assert.True(promotion.EndDate < promotion.StartDate);
        }

        [Fact]
        public void Name_Required_ShouldSet()
        {
            var promotion = new Promotion
            (
                "Black Friday",
                30,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(2),
                []
            );

            Assert.Equal("Black Friday", promotion.Name);
        }
    }
}
