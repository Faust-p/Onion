namespace Domain.Entities.Requests.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Date { get; }
        Guid RequestId { get; }
    }
}
