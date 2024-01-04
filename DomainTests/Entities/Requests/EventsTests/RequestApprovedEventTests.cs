using Domain.Entities.Requests.Events;
using System.Reflection;
namespace DomainTests.Entities.Requests.EventsTests;

public class RequestApprovedEventTests
{
    [Fact]
    public void Create_ShouldCreateValidInstance()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var requestApprovedEvent = RequestApprovedEvent.Create(requestId);

        // Assert
        Assert.NotNull(requestApprovedEvent);
        Assert.NotEqual(Guid.Empty, requestApprovedEvent.Id);
        Assert.Equal(requestId, requestApprovedEvent.RequestId);
    }

    [Fact]
    public void Create_ShouldSetDateToUtcNow()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var requestApprovedEvent = RequestApprovedEvent.Create(requestId);

        // Assert
        Assert.InRange(DateTime.UtcNow - requestApprovedEvent.Date, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Create_WithEmptyRequestId_ShouldThrowArgumentNullException()
    {
        // Arrange
        Guid emptyRequestId = Guid.Empty;

        // Act, Assert
        Assert.Throws<ArgumentNullException>(() => RequestApprovedEvent.Create(emptyRequestId));
    }
    [Fact]
    public void ConstructorPassedEmptyIdThrowsArgumentNullException()
    {
        Guid emptyId = Guid.Empty;
        Guid validRequestId = Guid.NewGuid();

        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
    
        var exception = Assert.Throws<TargetInvocationException>(() =>
            typeof(RequestApprovedEvent).GetConstructor(flags, null, new [] { typeof(Guid), typeof(Guid) }, null)
                ?.Invoke(new object[] { emptyId, validRequestId }));

        Assert.IsType<ArgumentNullException>(exception.InnerException);
        Assert.Equal("id", ((ArgumentNullException)exception.InnerException).ParamName);
    }
}