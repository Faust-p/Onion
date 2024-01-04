namespace Domain.Entities
{
    
    public class Email
    {
        public string Value { get; private set; }
        public Email(string value)
        {
            if (!value.Contains('@'))
            {
                throw new ArgumentException("Email указан неверно", nameof(value));
            }

            Value = value;
        }
    }
}
