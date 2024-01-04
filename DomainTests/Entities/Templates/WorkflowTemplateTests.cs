using Domain.Entities.Requests;
using Domain.Entities.Templates;
using Domain.Entities.Requests.Events;
using Domain.Entities;
using System.Reflection;

namespace DomainTests.Entities.Templates;

public class WorkflowTemplateTests
{
    [Fact]
        public void Create_ValidArguments_Success()
        {
            // Arrange
            var name = "Template 1";

            // Act
            var workflowTemplate = WorkflowTemplate.Create(name);

            // Assert
            Assert.NotNull(workflowTemplate);
            Assert.Equal(name, workflowTemplate.Name);
            Assert.Empty(workflowTemplate.Steps);
        }

        [Fact]
        public void Create_NullName_ThrowsArgumentNullException()
        {
            // Arrange
            string name = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => WorkflowTemplate.Create(name));
        }

        [Fact]
        public void SetName_ValidName_Success()
        {
            // Arrange
            var workflowTemplate = WorkflowTemplate.Create("Template 1");
            var newName = "Updated Template";

            // Act
            workflowTemplate.SetName(newName);

            // Assert
            Assert.Equal(newName, workflowTemplate.Name);
        }

        [Fact]
        public void SetName_NullName_ThrowsArgumentNullException()
        {
            // Arrange
            var workflowTemplate = WorkflowTemplate.Create("Template 1");
            string newName = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => workflowTemplate.SetName(newName));
        }

        [Fact]
        public void CreateRequest_ValidArguments_Success()
        {
            // Arrange
            var workflowTemplate = WorkflowTemplate.Create("Template 1");
            var user = User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid());
            var document = Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789");

            // Act
            var request = workflowTemplate.CreateRequest(user, document);

            // Assert
            Assert.NotNull(request);
            Assert.Equal(user, request.User);
            Assert.Equal(document, request.Document);
            Assert.Equal(workflowTemplate.Name, request.Workflow.Name);
            Assert.NotEqual(Guid.Empty, request.Workflow.Id);
        }
        [Fact]
        public void PrivateConstructor_EmptyGuid_ThrowsArgumentNullException()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "Test Template";
            var steps = new List<WorkflowStepTemplate>();
            
            var constructorInfo = typeof(WorkflowTemplate)
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null,
                    new[] { typeof(Guid), typeof(string), typeof(List<WorkflowStepTemplate>) }, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => 
                constructorInfo.Invoke(new object[] { id, name, steps }));
            
            Assert.NotNull(exception.InnerException);
            Assert.IsType<ArgumentNullException>(exception.InnerException);
            Assert.Equal("id", ((ArgumentNullException)exception.InnerException).ParamName);
        }
}