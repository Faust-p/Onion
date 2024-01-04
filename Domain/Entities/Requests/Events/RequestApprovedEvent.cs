namespace Domain.Entities.Requests.Events
{
    public class RequestApprovedEvent : IEvent
    {
        public Guid Id { get; }
        public DateTime Date { get; }
        public Guid RequestId { get; }

        private RequestApprovedEvent(Guid id, Guid requestId)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            if (requestId == Guid.Empty)
                throw new ArgumentNullException(nameof(requestId));

            Id = id;
            Date = DateTime.UtcNow;
            RequestId = requestId;
        }

        public static RequestApprovedEvent Create(Guid requestId)
        {
            return new RequestApprovedEvent(Guid.NewGuid(), requestId);
        }
    }
}
