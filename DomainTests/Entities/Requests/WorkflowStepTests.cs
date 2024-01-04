using Domain.Entities.Requests;
using Domain.Entities.Requests.Events;
using Domain.Entities.Templates;
using Domain.Entities;
using System.Reflection;

namespace DomainTests.Entities.Requests;

public class WorkflowStepTests
{
    [Fact]
        public void CreateWithUserId_ValidArguments_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Step 1";
            var status = Status.Pending;
            var userId = Guid.NewGuid();
            var comment = "Test comment";

            // Act
            var step = WorkflowStep.CreateWithUserId(name, status, userId, comment);

            // Assert
            Assert.NotNull(step);
            Assert.Equal(name, step.Name);
            Assert.Equal(status, step.Status);
            Assert.Equal(userId, step.UserId);
            Assert.Equal(comment, step.Comment);
        }

        [Fact]
        public void CreateWithRoleId_ValidArguments_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Step 2";
            var status = Status.Approve;
            var roleId = Guid.NewGuid();
            var comment = "Another comment";

            // Act
            var step = WorkflowStep.CreateWithRoleId(name, status, roleId, comment);

            // Assert
            Assert.NotNull(step);
            Assert.Equal(name, step.Name);
            Assert.Equal(status, step.Status);
            Assert.Equal(roleId, step.RoleId);
            Assert.Equal(comment, step.Comment);
        }

        [Fact]
        public void PrivateConstructor_ValidArguments_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Step 3";
            var status = Status.Pending;
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var comment = "Another comment";

            // Act
            var step = Activator.CreateInstance(typeof(WorkflowStep), BindingFlags.NonPublic | BindingFlags.Instance, null,
                new object[] { id, name, status, comment, userId, roleId }, null) as WorkflowStep;

            // Assert
            Assert.NotNull(step);
            Assert.Equal(id, step.Id);
            Assert.Equal(name, step.Name);
            Assert.Equal(status, step.Status);
            Assert.Equal(userId, step.UserId);
            Assert.Equal(roleId, step.RoleId);
            Assert.Equal(comment, step.Comment);
        }
        [Fact]
        public void PrivateConstructor_WithEmptyId_ThrowsArgumentNullException()
        {
            // Arrange
            var emptyId = Guid.Empty;
            var validName = "Valid Name";
            var validStatus = Status.Pending;
            var validComment = "Valid Comment";

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => 
                Activator.CreateInstance(typeof(WorkflowStep), BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new object?[] { emptyId, validName, validStatus, validComment, null, null }, null));

            Assert.IsType<ArgumentNullException>(exception.InnerException);
            Assert.Equal("id", ((ArgumentNullException)exception.InnerException).ParamName);
        }
}