using Domain.Entities.Requests.Events;
using System.Reflection;
namespace DomainTests.Entities.Requests.EventsTests;

public class RequestCreateEventTests
{
    [Fact]
    public void Create_ShouldCreateValidInstance()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var requestCreateEvent = RequestCreateEvent.Create(requestId);

        // Assert
        Assert.NotNull(requestCreateEvent);
        Assert.NotEqual(Guid.Empty, requestCreateEvent.Id);
        Assert.Equal(requestId, requestCreateEvent.RequestId);
    }

    [Fact]
    public void Create_ShouldSetDateToUtcNow()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var requestCreateEvent = RequestCreateEvent.Create(requestId);

        // Assert
        Assert.InRange(DateTime.UtcNow - requestCreateEvent.Date, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Create_WithEmptyRequestId_ShouldThrowArgumentNullException()
    {
        // Arrange
        Guid emptyRequestId = Guid.Empty;

        // Act, Assert
        Assert.Throws<ArgumentNullException>(() => RequestCreateEvent.Create(emptyRequestId));
    }
    [Fact]
    public void PrivateConstructor_WithEmptyId_ShouldThrowArgumentNullException()
    {
        Guid validRequestId = Guid.NewGuid();
        Guid emptyId = Guid.Empty;
    
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        ConstructorInfo constructor = typeof(RequestCreateEvent).GetConstructor(flags, null, new[] { typeof(Guid), typeof(Guid) }, null);

        Action action = () => constructor.Invoke(new object[] { emptyId, validRequestId });

        var exception = Assert.Throws<TargetInvocationException>(action);
        Assert.IsType<ArgumentNullException>(exception.InnerException);
        Assert.Equal("id", ((ArgumentNullException)exception.InnerException).ParamName);
    }
}