namespace Domain.Entities
{
    public class Role
    {
        public Guid Id { get; private init; }
        public string Name { get; private set; } = null!;

        public Role(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            Id = id;
            SetName(name);
        }

        public static Role Create(string name)
        {
            return new Role(Guid.NewGuid(), name);
        }

        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
