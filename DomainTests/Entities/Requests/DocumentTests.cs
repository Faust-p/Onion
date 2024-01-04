using Domain.Entities.Requests;
using Domain.Entities;

namespace DomainTests.Entities.Requests;

public class DocumentTests
{
    [Fact]
        public void Document_Creation_Successful()
        {
            // Arrange
            var name = "John Doe";
            var email = new Email("john@example.com");
            var age = new DateTime(1990, 1, 1);
            var phoneNumber = "1234567890";

            // Act
            var document = Document.Create(name, email, age, phoneNumber);

            // Assert
            Assert.Equal(name, document.Name);
            Assert.Equal(email, document.Email);
            Assert.Equal(age, document.Age);
            Assert.Equal(phoneNumber, document.PhoneNumber);
        }

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var document = new Document("John Doe", new Email("john@example.com"), new DateTime(1990, 1, 1), "1234567890");

            // Act
            document.SetName("Jane Doe");

            // Assert
            Assert.Equal("Jane Doe", document.Name);
        }

        [Fact]
        public void SetEmail_ValidEmail_SetsEmail()
        {
            // Arrange
            var document = new Document("John Doe", new Email("john@example.com"), new DateTime(1990, 1, 1), "1234567890");
            var newEmail = new Email("jane@example.com");

            // Act
            document.SetEmail(newEmail);

            // Assert
            Assert.Equal(newEmail, document.Email);
        }

        [Fact]
        public void SetAge_ValidAge_SetsAge()
        {
            // Arrange
            var document = new Document("John Doe", new Email("john@example.com"), new DateTime(1990, 1, 1), "1234567890");
            var newAge = new DateTime(1985, 5, 10);

            // Act
            document.SetAge(newAge);

            // Assert
            Assert.Equal(newAge, document.Age);
        }

        [Fact]
        public void SetAge_InvalidAge_ThrowsArgumentException()
        {
            // Arrange
            var document = new Document("John Doe", new Email("john@example.com"), new DateTime(1990, 1, 1), "1234567890");
            var invalidAge = DateTime.Now.AddYears(1); // Set an age in the future

            // Act & Assert
            Assert.Throws<ArgumentException>(() => document.SetAge(invalidAge));
        }

        [Fact]
        public void SetPhoneNumber_ValidPhoneNumber_SetsPhoneNumber()
        {
            // Arrange
            var document = new Document("John Doe", new Email("john@example.com"), new DateTime(1990, 1, 1), "1234567890");
            var newPhoneNumber = "9876543210";

            // Act
            document.SetPhoneNumber(newPhoneNumber);

            // Assert
            Assert.Equal(newPhoneNumber, document.PhoneNumber);
        }

        [Fact]
        public void SetPhoneNumber_NullPhoneNumber_ThrowsArgumentNullException()
        {
            // Arrange
            var document = new Document("John Doe", new Email("john@example.com"), new DateTime(1990, 1, 1), "1234567890");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => document.SetPhoneNumber(null));
        }
}