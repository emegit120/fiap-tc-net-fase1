using FIAPTechChallenge.Domain.Entities;
using Xunit;

public class UserTests
{
    [Fact]
    public void Constructor_ValidData_ShouldCreateUser()
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        Assert.Equal("Nome", user.Name);
        Assert.Equal("email@email.com", user.Email);
        Assert.Equal(1, user.RoleId);
        Assert.False(string.IsNullOrWhiteSpace(user.Password)); // Senha deve estar criptografada
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("invalido")]
    public void SetEmail_InvalidEmail_ShouldThrow(string invalidEmail)
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        Assert.Throws<ArgumentException>(() => user.SetEmail(invalidEmail));
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("abcdefghi")]
    [InlineData("12345678")]
    public void SetPassword_InvalidPassword_ShouldThrow(string invalidPassword)
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        Assert.Throws<ArgumentException>(() => user.SetPassword(invalidPassword));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SetName_InvalidName_ShouldThrow(string invalidName)
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        Assert.Throws<ArgumentException>(() => user.SetName(invalidName));
    }

    [Fact]
    public void SetName_ValidName_ShouldUpdateName()
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        user.SetName("Novo Nome");
        Assert.Equal("Novo Nome", user.Name);
    }

    [Fact]
    public void SetEmail_ValidEmail_ShouldUpdateEmail()
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        user.SetEmail("novo@email.com");
        Assert.Equal("novo@email.com", user.Email);
    }

    [Fact]
    public void SetPassword_ValidPassword_ShouldHashPassword()
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        var oldPassword = user.Password;
        user.SetPassword("NovaSenha@123");
        Assert.NotEqual(oldPassword, user.Password);
        Assert.False(string.IsNullOrWhiteSpace(user.Password));
    }

    [Fact]
    public void Games_Collection_ShouldBeInstantiated()
    {
        var user = new User("Nome", "email@email.com", "Senha@123", 1);
        Assert.NotNull(user.Games);
        Assert.Empty(user.Games);
    }
}
