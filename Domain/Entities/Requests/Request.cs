using Domain.Entities.Requests.Events;

namespace Domain.Entities.Requests
{
    public class Request
    {
        private List<IEvent> _events = new List<IEvent>();
        public Guid Id { get; private init; }
        public User User { get; private set; } = null!;
        public Document Document { get; private set; } = null!;
        public Workflow Workflow { get; private set; } = null!;
        public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();

        private Request(Guid id, User user, Document document, Workflow workflow)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            Id = id;
            SetUser(user);
            SetDocument(document);
            SetWorkflow(workflow);

        }
        public static Request Create(User user, Document document, Workflow workflow)
        {
            return new Request(Guid.NewGuid(), user, document, workflow);
        }

        public void SetUser(User user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        public void SetDocument(Document document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public void SetWorkflow(Workflow workflow)
        {
            Workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
        }

        public void Restart()
        {
            foreach (var step in Workflow.Steps)
            {
                step.Status = Status.Pending;
            }
        }

        public void Approve()
        {
            var statusPending = Workflow.Steps.FirstOrDefault(step => step.Status == Status.Pending);
            ArgumentNullException.ThrowIfNull(statusPending, "Не найдено состояние ожидания");

            statusPending.Status = Status.Approve;
            _events.Add(RequestApprovedEvent.Create(Id));
        }

        public void Reject()
        {
            var statusPending = Workflow.Steps.FirstOrDefault(step => step.Status == Status.Pending);
            ArgumentNullException.ThrowIfNull(statusPending, "Не найдено состояние ожидания");

            statusPending.Status = Status.Reject;
            _events.Add(RequestRejectEvent.Create(Id));
        }
    }
}