using Domain.Entities.Requests;

namespace Domain.Entities.Templates
{
    public class WorkflowTemplate
    {
        private List<WorkflowStepTemplate> _steps;
        public Guid Id { get; private init; }
        public string Name { get; private set; }
        public IReadOnlyList<WorkflowStepTemplate> Steps => _steps;
        


        private WorkflowTemplate(Guid id, string name, List<WorkflowStepTemplate> steps)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            ArgumentNullException.ThrowIfNull(steps);

            Id = id;
            SetName(name);
            _steps = steps;
        }

        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public static WorkflowTemplate Create(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            var steps = new List<WorkflowStepTemplate>();
            return new WorkflowTemplate(Guid.NewGuid(), name, steps);
        }

        public Request CreateRequest(User user, Document document)
        {
            return Request.Create(user, document, Workflow.Create(Name, Id));
        }
    }
}
