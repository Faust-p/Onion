namespace Domain.Entities.Requests
{
    public class WorkflowStep
    {
        public Guid Id { get; }
        public string Name { get; private set; } = null!;
        public int Order { get; private set; } = 0;
        public Status Status { get; set; }
        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }
        public string? Comment { get; set; }

        private WorkflowStep(Guid id, string name, Status status, string? comment, Guid? userId = null, Guid? roleId = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            Id = id;
            SetName(name);
            SetOrder();
            Status = status;
            UserId = userId;
            RoleId = roleId;
            SetComment(comment);
        }

        public static WorkflowStep CreateWithUserId(string name, Status status, Guid userId, string? comment)
        {
            return new WorkflowStep(Guid.NewGuid(), name, status, comment, userId: userId);
        }

        public static WorkflowStep CreateWithRoleId(string name, Status status, Guid roleId, string? comment)
        {
            return new WorkflowStep(Guid.NewGuid(), name, status, comment, roleId: roleId);
        }

        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        private void SetOrder()
        {
            Order++;
        }

        public void SetComment(string? comment)
        {
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
        }
    }
}
