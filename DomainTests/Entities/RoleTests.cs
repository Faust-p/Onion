using Domain.Entities;

namespace DomainTests.Entities;

public class RoleTests
{
    [Fact]
    public void Constructor_WithValidArguments_SetsProperties()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Admin";

        // Act
        var role = new Role(id, name);

        // Assert
        Assert.Equal(id, role.Id);
        Assert.Equal(name, role.Name);
    }

    [Fact]
    public void Constructor_WithEmptyId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid id = Guid.Empty;
        string name = "Admin";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Role(id, name));
    }

    [Fact]
    public void Create_ReturnsRoleWithNewGuid()
    {
        // Arrange
        string name = "User";

        // Act
        var role = Role.Create(name);

        // Assert
        Assert.NotEqual(Guid.Empty, role.Id);
        Assert.Equal(name, role.Name);
    }

    [Fact]
    public void SetName_WithValidName_SetsName()
    {
        // Arrange
        var role = new Role(Guid.NewGuid(), "Moderator");
        string newName = "UpdatedModerator";

        // Act
        role.SetName(newName);

        // Assert
        Assert.Equal(newName, role.Name);
    }

    [Fact]
    public void SetName_WithNullName_ThrowsArgumentNullException()
    {
        // Arrange
        var role = new Role(Guid.NewGuid(), "Editor");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => role.SetName(null));
    }
}