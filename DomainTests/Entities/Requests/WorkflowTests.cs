using Domain.Entities.Requests;
using Domain.Entities.Requests.Events;
using Domain.Entities;
using System.Reflection;

namespace DomainTests.Entities.Requests;

public class WorkflowTests
{
        [Fact]
        public void Create_Workflow_Success()
        {
            // Arrange
            Guid workflowId = Guid.NewGuid();
            Guid templateId = Guid.NewGuid();
            string workflowName = "Sample Workflow";
            List<WorkflowStep> steps = new List<WorkflowStep>();

            // Act
            Workflow workflow = Workflow.Create(workflowName, templateId);

            // Assert
            Assert.NotNull(workflow);
            Assert.Equal(workflowName, workflow.Name);
            Assert.Equal(templateId, workflow.WorkflowTemplateId);
            Assert.Empty(workflow.Steps);
        }

        [Fact]
        public void SetName_Workflow_Success()
        {
            // Arrange
            Workflow workflow = Workflow.Create("Sample Workflow", Guid.NewGuid());
            string newName = "Updated Workflow";

            // Act
            workflow.SetName(newName);

            // Assert
            Assert.Equal(newName, workflow.Name);
        }

        [Fact]
        public void Restart_Workflow_Success()
        {
            // Arrange
            Workflow workflow = Workflow.Create("Sample Workflow", Guid.NewGuid());
            workflow.AddStep("Step1", Status.Pending, null, Guid.NewGuid(), "Comment");

            // Act
            workflow.Restart();

            // Assert
            Assert.Empty(workflow.Steps);
        }

        [Fact]
        public void AddStep_Workflow_Success()
        {
            // Arrange
            Workflow workflow = Workflow.Create("Sample Workflow", Guid.NewGuid());
            Guid roleId = Guid.NewGuid();

            // Act
            workflow.AddStep("Step1", Status.Pending, null, roleId, "Comment");

            // Assert
            Assert.Single(workflow.Steps);
            Assert.Equal("Step1", workflow.Steps.First().Name);
            Assert.Equal(Status.Pending, workflow.Steps.First().Status);
            Assert.Null(workflow.Steps.First().UserId);
            Assert.Equal(roleId, workflow.Steps.First().RoleId);
            Assert.Equal("Comment", workflow.Steps.First().Comment);
        }

        [Fact]
        public void IsApprove_Workflow_Success()
        {
            // Arrange
            Workflow workflow = Workflow.Create("Sample Workflow", Guid.NewGuid());
            
            workflow.AddStep("Step2", Status.Approve, null, Guid.NewGuid(), "Comment");

            // Act
            bool isApprove = workflow.IsApprove();

            // Assert
            Assert.True(isApprove);
        }


        [Fact]
        public void IsReject_Workflow_Success()
        {
            // Arrange
            Workflow workflow = Workflow.Create("Sample Workflow", Guid.NewGuid());
            
            workflow.AddStep("Step2", Status.Reject, null, Guid.NewGuid(), "Comment");

            // Act
            bool isReject = workflow.IsReject();

            // Assert
            Assert.True(isReject);
        }
        [Fact]
        public void Constructor_WithEmptyId_ThrowsArgumentNullException()
        {
            // Arrange
            Guid emptyId = Guid.Empty;
            Guid validWorkflowTemplateId = Guid.NewGuid();
            string validName = "Valid Workflow Name";
            List<WorkflowStep> steps = new List<WorkflowStep>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Workflow(emptyId, validWorkflowTemplateId, validName, steps));
    
            Assert.Equal("id", exception.ParamName);
        }
        [Fact]
        public void Constructor_WithEmptyWorkflowTemplateId_ThrowsArgumentNullException()
        {
            // Arrange
            Guid validId = Guid.NewGuid();
            Guid emptyWorkflowTemplateId = Guid.Empty;
            string validName = "Valid Workflow Name";
            List<WorkflowStep> steps = new List<WorkflowStep>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Workflow(validId, emptyWorkflowTemplateId, validName, steps));
    
            Assert.Equal("workflowTemplateId", exception.ParamName);
        }
}