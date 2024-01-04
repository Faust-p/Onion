namespace Domain.Entities.Requests
{
    public class Document
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public DateTime Age { get; private set; }
        public string PhoneNumber { get; private set; }

        public Document(string name, Email email, DateTime age, string phoneNumber)
        {
            SetName(name);
            SetEmail(email);
            SetAge(age);
            SetPhoneNumber(phoneNumber);
        }

        public static Document Create(string name, Email email, DateTime age, string phoneNumber)
        {
            return new Document(name, email, age, phoneNumber);
        }

        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void SetEmail(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public void SetAge(DateTime age)
        {
            if (age < DateTime.Now.AddYears(-100) || age > DateTime.Now)
            {
                throw new ArgumentException("Возраст указан неверно.", nameof(age));
            }

            Age = age;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        }

    }
}
