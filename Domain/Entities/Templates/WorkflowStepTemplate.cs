namespace Domain.Entities.Templates
{
    public class WorkflowStepTemplate
    {
        public string Name { get; private init; }
        public int Order { get; private init; }
        public Guid UserId { get; private init; }
        public Guid RoleId { get; private init; }

        private WorkflowStepTemplate(string name, int order, Guid userId, Guid roleId)
        {
            if (roleId == Guid.Empty)
                throw new ArgumentNullException(nameof(roleId));

            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            Name = name ?? throw new ArgumentNullException(nameof(name));

            if (order < 0)
                throw new ArgumentOutOfRangeException("Order >= 0");

            RoleId = roleId;
            UserId = userId;
            Name = name;
            Order = order;
        }

        public static WorkflowStepTemplate Create(string name, int order, Guid userId, Guid roleId)
        {
            return new WorkflowStepTemplate(name, order, userId, roleId);
        }
    }
}
