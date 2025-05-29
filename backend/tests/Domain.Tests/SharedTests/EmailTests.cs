using Domain.Shared;

namespace Domain.Tests.SharedTests;

public sealed class EmailTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    internal void Email_WhenValueIsInvalid_ShouldThrowException()
    {
        // Arrange
        string invalidEmail = Guid.NewGuid().ToString();

        // Act
        void action() => _ = new Email(invalidEmail);

        // Assert
        Should.Throw<DomainException>(action)
            .Message.ShouldBe("Invalid email address.");
    }

    [Fact]
    internal void Email_WhenValueIsValid_ShouldNotThrowException()
    {
        // Arrange
        string email = _faker.Internet.Email();

        // Act
        void action() => _ = new Email(email);

        // Assert
        Should.NotThrow(action);
    }
}