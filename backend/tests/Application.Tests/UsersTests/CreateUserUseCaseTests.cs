using Application.DTOs;
using Application.Results;
using Application.Users;
using Domain.Users;

namespace Application.Tests.UsersTests;

public sealed class CreateUserUseCaseTests
{
    private readonly Faker _faker = new("pt_BR");
    private readonly CreateUserUseCase _sut;

    public CreateUserUseCaseTests()
    {
        _sut = new( );
    }

    [Fact]
    internal async Task Execute_WhenInvokedWithoutPassword_ShouldReturnAnErrorResult()
    {
        // Arrange
        CreateUserDTO createUserDTO = new(
            _faker.Person.FirstName,
            _faker.Person.LastName,
            _faker.Internet.Email(),
            string.Empty
        );

        // Act
        Result<User> response = await _sut.Execute(createUserDTO);

        // Assert
        response.Errors.ShouldNotBe(null);
        response.Errors.ShouldContain("Password is required");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("abc")]
    internal async Task Execute_WhenInvokedWithAShortPassword_ShouldReturnAErrorResult(string password)
    {
        // Arrange
        CreateUserDTO createUserDTO = new(
                    _faker.Person.FirstName,
                    _faker.Person.LastName,
                    _faker.Internet.Email(),
                    password
                );
        // Act
        Result<User> response = await _sut.Execute(createUserDTO);

        // Assert
        response.Errors.ShouldNotBe(null);
        response.Errors.ShouldContain("Password must be at least 16 characters");
    }

    [Fact]
    internal async Task Execute_WhenInvokedWithCorrectData_ShouldReturnASuccessResult()
    {
        // Arrange
        CreateUserDTO createUserDTO = new(
                    _faker.Person.FirstName,
                    _faker.Person.LastName,
                    _faker.Internet.Email(),
                    Guid.NewGuid().ToString()[..16]
                );
        // Act
        Result<User> response = await _sut.Execute(createUserDTO);

        // Assert
        response.IsSuccess.ShouldBeTrue();
        response.Value.ShouldBeOfType<User>();
    }
}