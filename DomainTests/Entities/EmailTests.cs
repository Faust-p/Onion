using Domain.Entities;

namespace DomainTests.Entities;

public class EmailTests
{
    [Fact]
    public void Email_WithValidValue_ShouldSetCorrectValue()
    {
        // Arrange
        string validEmail = "test@example.com";

        // Act
        var email = new Email(validEmail);

        // Assert
        Assert.Equal(validEmail, email.Value);
    }

    [Theory]
    [InlineData("invalidemail")]
    [InlineData("")]
    public void Email_WithInvalidValue_ShouldThrowArgumentException(string invalidEmail)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
    }
}