using Domain.Shared;

namespace Domain.Tests.SharedTests;

public sealed class UsernameTests
{
    [Theory]
    [InlineData("someuser")]
    [InlineData("awesomeuser")]
    [InlineData("a_random_user")]
    internal void Username_WhenValueIsValid_ShouldNotThrowException(string username)
    {
        // Act
        void action() => _ = new Username(username);

        // Assert
        Should.NotThrow(action);
    }

    [Theory]
    [InlineData("a_very_big_username")]
    [InlineData("a")]
    [InlineData("bc")]
    [InlineData("with space")]
    [InlineData("WithUppercase")]
    internal void Username_WhenInvalid_ShouldThrowDomainException(string username)
    {
        // Act
        void action() => _ = new Username(username);

        // Assert
        Should.Throw<DomainException>(action);
    }
}