using Domain.Entities;
using System.Reflection;
namespace DomainTests.Entities;

public class UserTests
{
    [Fact]
    public void Create_WithValidArguments_ReturnsUserWithPropertiesSet()
    {
        // Arrange
        string name = "John Doe";
        Email email = new Email("john.doe@example.com");
        Guid roleId = Guid.NewGuid();

        // Act
        var user = User.Create(name, email, roleId);

        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(roleId, user.RoleId);
    }
    
    [Fact]
    public void Create_WithEmptyName_ThrowsArgumentNullException()
    {
        // Arrange
        string name = "";
        Email email = new Email("john.doe@example.com");
        Guid roleId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => User.Create(name, email, roleId));
    }

    [Fact]
    public void Create_WithNullEmail_ThrowsArgumentNullException()
    {
        // Arrange
        string name = "John Doe";
        Email email = null;
        Guid roleId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => User.Create(name, email, roleId));
    }

    [Fact]
    public void SetName_WithValidName_SetsName()
    {
        // Arrange
        var user = User.Create("Alice", new Email("alice@example.com"), Guid.NewGuid());
        string newName = "UpdatedAlice";

        // Act
        user.SetName(newName);

        // Assert
        Assert.Equal(newName, user.Name);
    }

    [Fact]
    public void SetName_WithNullName_ThrowsArgumentNullException()
    {
        // Arrange
        var user = User.Create("Bob", new Email("bob@example.com"), Guid.NewGuid());

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => user.SetName(null));
    }

    [Fact]
    public void SetEmail_WithValidEmail_SetsEmail()
    {
        // Arrange
        var user = User.Create("Charlie", new Email("charlie@example.com"), Guid.NewGuid());
        var newEmail = new Email("updated.charlie@example.com");

        // Act
        user.SetEmail(newEmail);

        // Assert
        Assert.Equal(newEmail, user.Email);
    }

    [Fact]
    public void SetEmail_WithNullEmail_ThrowsArgumentNullException()
    {
        // Arrange
        var user = User.Create("David", new Email("david@example.com"), Guid.NewGuid());

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => user.SetEmail(null));
    }

    [Fact]
    public void SetRole_WithValidRoleId_SetsRoleId()
    {
        // Arrange
        var user = User.Create("Eve", new Email("eve@example.com"), Guid.NewGuid());
        Guid newRoleId = Guid.NewGuid();

        // Act
        user.SetRole(newRoleId);

        // Assert
        Assert.Equal(newRoleId, user.RoleId);
    }

    [Fact]
    public void SetRole_WithEmptyRoleId_ThrowsArgumentNullException()
    {
        // Arrange
        var user = User.Create("Frank", new Email("frank@example.com"), Guid.NewGuid());

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => user.SetRole(Guid.Empty));
    }
    [Fact]
    public void Constructor_WithEmptyId_ThrowsArgumentNullException()
    {
        // Arrange
        string name = "John Doe";
        Email email = new Email("john.doe@example.com");
        Guid roleId = Guid.NewGuid();
        var constructor = typeof(User).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null, new[] { typeof(Guid), typeof(string), typeof(Email), typeof(Guid) }, null);
    
        // Act & Assert
        var parameters = new object[] { Guid.Empty, name, email, roleId };
        Assert.Throws<TargetInvocationException>(() => constructor.Invoke(parameters));
    }

}