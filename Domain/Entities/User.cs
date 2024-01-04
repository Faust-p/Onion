namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; private init; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Guid RoleId { get; private set; }

        private User(Guid id, string name, Email email, Guid roleId)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            Id = id;
            SetName(name);
            SetEmail(email);
            SetRole(roleId);
        }

        public static User Create(string name, Email email, Guid roleId)
        {
            return new User(Guid.NewGuid(), name, email, roleId);
        }

        public void SetName(string name)
        {;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void SetEmail(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public void SetRole(Guid roleId)
        {
            if (roleId == Guid.Empty)
                throw new ArgumentNullException(nameof(roleId));

            RoleId = roleId;
        }
    }
}
