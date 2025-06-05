using Application.Authentication;
using Application.DTOs;
using Application.Results;
using Domain.Users;
using System.Threading.Tasks;

namespace Application.Tests.AuthenticationTests;

public sealed class LoginUseCaseTests
{
    private readonly Faker _faker = new();
    private readonly Mock<IAuthService> _authServiceMock = new();
    private readonly LoginUseCase _sut;

    public LoginUseCaseTests()
    {
        _sut = new(_authServiceMock.Object);
    }

    [Fact]
    public async Task Execute_WhenInvoked_ShouldInvokeLoginMethodOfAuthService()
    {
        // Arrange
        LoginRequestDTO request = new(_faker.Internet.Email(),
            _faker.Internet.Password());

        _authServiceMock.Setup(_ => _.Login(request.Email, request.Password))
            .ReturnsAsync(Result.Success(new User()));

        // Act
        await _sut.Execute(request);

        // Assert
        _authServiceMock.Verify(_ =>
            _.Login(request.Email, request.Password), Times.Once);
    }

    [Fact]
    public async Task Execute_WhenInvokedAndUserDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        LoginRequestDTO request = new(_faker.Internet.Email(),
            _faker.Internet.Password());

        _authServiceMock.Setup(_ => _.Login(request.Email, request.Password))
            .ReturnsAsync(Result.Failure<User>("User not found"));

        // Act
        Result<string> response = await _sut.Execute(request);

        // Assert
        response.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public async Task Execute_WhenInvokedAndUserExists_ShouldReturnSuccess()
    {
        // Arrange
        LoginRequestDTO request = new(_faker.Internet.Email(),
            _faker.Internet.Password());

        _authServiceMock.Setup(_ => _.Login(request.Email, request.Password))
            .ReturnsAsync(Result.Success(new User()));

        // Act
        Result<string> response = await _sut.Execute(request);

        // Assert
        response.IsSuccess.ShouldBeTrue();
    }
}
