using Domain.Entities.Requests;
using Domain.Entities.Templates;
using Domain.Entities.Requests.Events;
using Domain.Entities;
using System.Reflection;

namespace DomainTests.Entities.Templates;

public class WorkflowStepTemplateTests
{
    [Fact]
        public void Create_ValidArguments_Success()
        {
            // Arrange
            var name = "Step 1";
            var order = 1;
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            // Act
            var stepTemplate = WorkflowStepTemplate.Create(name, order, userId, roleId);

            // Assert
            Assert.NotNull(stepTemplate);
            Assert.Equal(name, stepTemplate.Name);
            Assert.Equal(order, stepTemplate.Order);
            Assert.Equal(userId, stepTemplate.UserId);
            Assert.Equal(roleId, stepTemplate.RoleId);
        }

        [Fact]
        public void Create_EmptyRoleId_ThrowsArgumentNullException()
        {
            // Arrange
            var name = "Step 1";
            var order = 1;
            var userId = Guid.NewGuid();
            var roleId = Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => WorkflowStepTemplate.Create(name, order, userId, roleId));
        }

        [Fact]
        public void Create_EmptyUserId_ThrowsArgumentNullException()
        {
            // Arrange
            var name = "Step 1";
            var order = 1;
            var userId = Guid.Empty;
            var roleId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => WorkflowStepTemplate.Create(name, order, userId, roleId));
        }

        [Fact]
        public void Create_NegativeOrder_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var name = "Step 1";
            var order = -1;
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => WorkflowStepTemplate.Create(name, order, userId, roleId));
        }

        [Fact]
        public void Create_NullName_ThrowsArgumentNullException()
        {
            // Arrange
            string name = null;
            var order = 1;
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => WorkflowStepTemplate.Create(name, order, userId, roleId));
        }
}