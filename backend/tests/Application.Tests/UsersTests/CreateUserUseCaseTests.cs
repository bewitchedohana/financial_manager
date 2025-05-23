using Application.DTOs;
using Application.Results;
using Application.Users;

namespace Application.Tests.UsersTests;

public sealed class CreateUserUseCaseTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    internal void Execute_WhenInvokedWithoutPassword_ShouldReturnAnErrorResult()
    {
        // Arrange
        CreateUserDTO createUserDTO = new(
            _faker.Internet.Email(),
            _faker.Person.FirstName,
            _faker.Person.LastName,
            string.Empty
        );

        // Act
        Result<UserDTO> response = new CreateUserUseCase().Execute(createUserDTO);

        // Assert
        response.Error.ShouldNotBe(null);
        response.Error!.Code.ShouldBe("BAD_REQUEST");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("abc")]
    internal void Execute_WhenInvokedWithAShortPassword_ShouldReturnAErrorResult(string password)
    {
        // Arrange
        CreateUserDTO createUserDTO = new(
                    _faker.Internet.Email(),
                    _faker.Person.FirstName,
                    _faker.Person.LastName,
                    password
                );
        // Act
        Result<UserDTO> response = new CreateUserUseCase().Execute(createUserDTO);

        // Assert
        response.Error.ShouldNotBe(null);
        response.Error!.Code.ShouldBe("BAD_REQUEST");
    }
}