using Domain.Entities.Requests.Events;
using System.Reflection;
namespace DomainTests.Entities.Requests.EventsTests;

public class RequestRejectEventTests
{
    [Fact]
    public void Create_ShouldCreateValidInstance()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var requestRejectEvent = RequestRejectEvent.Create(requestId);

        // Assert
        Assert.NotNull(requestRejectEvent);
        Assert.NotEqual(Guid.Empty, requestRejectEvent.Id);
        Assert.Equal(requestId, requestRejectEvent.RequestId);
    }

    [Fact]
    public void Create_ShouldSetDateToUtcNow()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var requestRejectEvent = RequestRejectEvent.Create(requestId);

        // Assert
        Assert.InRange(DateTime.UtcNow - requestRejectEvent.Date, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Create_WithEmptyRequestId_ShouldThrowArgumentNullException()
    {
        // Arrange
        Guid emptyRequestId = Guid.Empty;

        // Act, Assert
        Assert.Throws<ArgumentNullException>(() => RequestRejectEvent.Create(emptyRequestId));
    }
    [Fact]
    public void PrivateConstructor_WithEmptyId_ShouldThrowArgumentNullException()
    {
        Guid validRequestId = Guid.NewGuid();
        Guid emptyId = Guid.Empty;
    
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var constructor = typeof(RequestRejectEvent).GetConstructor(flags, null, new[] { typeof(Guid), typeof(Guid) }, null);
    
        // Act and Assert
        var exception = Assert.Throws<TargetInvocationException>(() => constructor.Invoke(new object[] { emptyId, validRequestId }));
        Assert.IsType<ArgumentNullException>(exception.InnerException);
        Assert.Equal("id", ((ArgumentNullException)exception.InnerException).ParamName);
    }
}