namespace Domain.Entities.Requests.Events
{
    public class RequestCreateEvent : IEvent
    {
        public Guid Id { get; }
        public DateTime Date { get; }
        public Guid RequestId { get; }

        private RequestCreateEvent(Guid id, Guid requestId)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            if (requestId == Guid.Empty)
                throw new ArgumentNullException(nameof(requestId));

            Id = id;
            Date = DateTime.UtcNow;
            RequestId = requestId;
        }

        public static RequestCreateEvent Create(Guid requestId)
        {
            return new RequestCreateEvent(Guid.NewGuid(), requestId);
        }
    }
}
