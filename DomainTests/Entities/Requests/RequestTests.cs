using Domain.Entities.Requests;
using Domain.Entities.Templates;
using Domain.Entities.Requests.Events;
using Domain.Entities;
using System.Reflection;

namespace DomainTests.Entities.Requests;

public class RequestTests
{
    [Fact]
        public void Create_ValidArguments_Success()
        {
            // Arrange
            var user = User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid());
            var document = Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789");
            var workflow = Workflow.Create("Workflow 1", Guid.NewGuid());

            // Act
            var request = Request.Create(user, document, workflow);

            // Assert
            Assert.NotNull(request);
            Assert.NotEqual(Guid.Empty, request.Id);
            Assert.Equal(user, request.User);
            Assert.Equal(document, request.Document);
            Assert.Equal(workflow, request.Workflow);
            Assert.Empty(request.Events);
        }

        [Fact]
        public void Create_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            User user = null;
            var document = Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789");
            var workflow = Workflow.Create("Workflow 1", Guid.NewGuid());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Request.Create(user, document, workflow));
        }

        [Fact]
        public void Create_NullDocument_ThrowsArgumentNullException()
        {
            // Arrange
            var user = User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid());
            Document document = null;
            var workflow = Workflow.Create("Workflow 1", Guid.NewGuid());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Request.Create(user, document, workflow));
        }

        [Fact]
        public void Create_NullWorkflow_ThrowsArgumentNullException()
        {
            // Arrange
            var user = User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid());
            var document = Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789");
            Workflow workflow = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Request.Create(user, document, workflow));
        }

        [Fact]
        public void SetUser_ValidUser_Success()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));
            var newUser = User.Create("Jane Doe", new Email("jane.doe@example.com"), Guid.NewGuid());

            // Act
            request.SetUser(newUser);

            // Assert
            Assert.Equal(newUser, request.User);
        }

        [Fact]
        public void SetUser_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));
            User newUser = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => request.SetUser(newUser));
        }

        [Fact]
        public void SetDocument_ValidDocument_Success()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));
            var newDocument = Document.Create("Document 2", new Email("doc2@example.com"), DateTime.Now, "987654321");

            // Act
            request.SetDocument(newDocument);

            // Assert
            Assert.Equal(newDocument, request.Document);
        }

        [Fact]
        public void SetDocument_NullDocument_ThrowsArgumentNullException()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));
            Document newDocument = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => request.SetDocument(newDocument));
        }

        [Fact]
        public void SetWorkflow_ValidWorkflow_Success()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));
            var newWorkflow = Workflow.Create("Workflow 2", Guid.NewGuid());

            // Act
            request.SetWorkflow(newWorkflow);

            // Assert
            Assert.Equal(newWorkflow, request.Workflow);
        }

        [Fact]
        public void SetWorkflow_NullWorkflow_ThrowsArgumentNullException()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));
            Workflow newWorkflow = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => request.SetWorkflow(newWorkflow));
        }

        [Fact]
        public void Restart_ValidRequest_Success()
        {
            // Arrange
            var workflow = Workflow.Create("Workflow 1", Guid.NewGuid());
            workflow.AddStep("Step 1", Status.Approve, null, Guid.Empty, "Initial Step");
            workflow.AddStep("Step 2", Status.Reject, null, Guid.Empty, "Second Step");
    
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                workflow);

            // Act
            request.Restart();

            // Assert
            Assert.All(request.Workflow.Steps, step => Assert.Equal(Status.Pending, step.Status));
        }

        [Fact]
        public void Approve_ValidRequest_Success()
        {
            // Arrange
            var workflow = Workflow.Create("Workflow 1", Guid.NewGuid());
            workflow.AddStep("Step 1", Status.Pending, null, Guid.Empty, "Initial Step");

            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                workflow);
            request.Restart();

            // Act
            request.Approve();

            // Assert
            Assert.Equal(Status.Approve, request.Workflow.Steps.First().Status);
            Assert.Single(request.Events);
            Assert.IsType<RequestApprovedEvent>(request.Events.First());
        }

        [Fact]
        public void Reject_ValidRequest_Success()
        {
            // Arrange
            var workflow = Workflow.Create("Workflow 1", Guid.NewGuid());
            workflow.AddStep("Step 1", Status.Pending, null, Guid.Empty, "Initial Step");

            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                workflow);
            request.Restart();

            // Act
            request.Reject();

            // Assert
            Assert.Equal(Status.Reject, request.Workflow.Steps.First().Status);
            Assert.Single(request.Events);
            Assert.IsType<RequestRejectEvent>(request.Events.First());
        }

        [Fact]
        public void Approve_NoPendingStep_ThrowsArgumentNullException()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => request.Approve());
        }

        [Fact]
        public void Reject_NoPendingStep_ThrowsArgumentNullException()
        {
            // Arrange
            var request = Request.Create(User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid()),
                                         Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789"),
                                         Workflow.Create("Workflow 1", Guid.NewGuid()));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => request.Reject());
        }
        [Fact]
        public void Constructor_IdIsEmptyGuid_ThrowsArgumentNullException()
        {
            // Arrange
            Guid id = Guid.Empty;
            var user = User.Create("John Doe", new Email("john.doe@example.com"), Guid.NewGuid());
            var document = Document.Create("Document 1", new Email("doc@example.com"), DateTime.Now, "123456789");
            var workflow = Workflow.Create("Workflow 1", Guid.NewGuid());
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var parameters = new object[] { id, user, document, workflow };

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => 
                typeof(Request).GetConstructor(flags, null, new Type[] { id.GetType(), user.GetType(), document.GetType(), workflow.GetType() }, null)
                    ?.Invoke(parameters));
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }
        
}