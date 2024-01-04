namespace Domain.Entities.Requests
{
    public class Workflow
    {
        private readonly List<WorkflowStep> _steps;
        public Guid Id { get; }
        public string Name { get; private set; } = null!;
        public Guid WorkflowTemplateId { get; private init; }
        public IReadOnlyCollection<WorkflowStep> Steps => _steps;

        public Workflow(Guid id, Guid workflowTemplateId, string name, List<WorkflowStep> steps)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            if (workflowTemplateId == Guid.Empty)
                throw new ArgumentNullException(nameof(workflowTemplateId));

            Id = id;
            SetName(name);
            WorkflowTemplateId = workflowTemplateId;
            _steps = steps;
        }

        public static Workflow Create(string name, Guid workflowTemplateId)
        {
            var steps = new List<WorkflowStep>();
            return new Workflow(Guid.NewGuid(), workflowTemplateId, name, steps);
        }

        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Restart()
        {
            _steps.Clear();
        }

        public void AddStep(string name, Status status, User? user, Guid roleId, string comment)
        {
            _steps.Add(user is not null && roleId != Guid.Empty
                ? WorkflowStep.CreateWithUserId(name, status, user.Id, comment)
                : WorkflowStep.CreateWithRoleId(name, status, roleId, comment));
        }

        public bool IsApprove()
        {
            return Steps.Any() && Steps.LastOrDefault()?.Status == Status.Approve
                && !Steps.Any(step => step.Status is Status.Reject or Status.Pending);
        }

        public bool IsReject()
        {
            return Steps.Any(step => step.Status == Status.Reject)
                && !Steps.Any(step => step.Status is Status.Approve or Status.Pending);
        }
    }
}
