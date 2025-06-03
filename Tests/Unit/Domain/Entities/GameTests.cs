using FIAPTechChallenge.Domain.Entities;
using Xunit;

public class GameTests
{
    [Fact]
    public void Constructor_ValidData_ShouldCreateGame()
    {
        var game = new Game("Nome", "Descrição", 10m);
        Assert.Equal("Nome", game.Name);
        Assert.Equal("Descrição", game.Description);
        Assert.Equal(10m, game.Price);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void SetName_InvalidName_ShouldThrow(string invalidName)
    {
        var game = new Game("Nome", "Descrição", 10m);
        Assert.Throws<ArgumentException>(() => game.SetName(invalidName));
    }

    [Fact]
    public void SetPrice_NegativeValue_ShouldThrow()
    {
        var game = new Game("Nome", "Descrição", 10m);
        Assert.Throws<ArgumentException>(() => game.SetPrice(-1));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void SetDescription_InvalidDescription_ShouldThrow(string invalidDescription)
    {
        var game = new Game("Nome", "Descrição", 10m);
        Assert.Throws<ArgumentException>(() => game.SetDescription(invalidDescription));
    }

    [Fact]
    public void SetName_ValidName_ShouldUpdateName()
    {
        var game = new Game("Nome", "Descrição", 10m);
        game.SetName("Novo Nome");
        Assert.Equal("Novo Nome", game.Name);
    }

    [Fact]
    public void SetDescription_ValidDescription_ShouldUpdateDescription()
    {
        var game = new Game("Nome", "Descrição", 10m);
        game.SetDescription("Nova Descrição");
        Assert.Equal("Nova Descrição", game.Description);
    }

    [Fact]
    public void SetPrice_ValidValue_ShouldUpdatePrice()
    {
        var game = new Game("Nome", "Descrição", 10m);
        game.SetPrice(99.99m);
        Assert.Equal(99.99m, game.Price);
    }

    [Fact]
    public void SetPrice_InvalidValue_ShouldUpdatePrice()
    {
        var game = new Game("Nome", "Descrição", 10m);
        game.SetPrice(0);
        Assert.Equal(0, game.Price);
    }
}
