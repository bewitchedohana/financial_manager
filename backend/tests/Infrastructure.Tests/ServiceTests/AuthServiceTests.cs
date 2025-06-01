using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Authentication;
using Application.Results;
using Bogus;
using Domain.Users;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Shouldly;

namespace Infrastructure.Tests.ServiceTests;

public sealed class AuthServiceTests
{
    private readonly Mock<IUserStore<User>> _userStoreMock = new();
    private readonly Mock<IOptions<AuthenticationOptions>> _authenticationOptionsMock =
        new();
    private readonly Mock<UserManager<User>> userManager;
    private readonly Faker _faker = new("pt_BR");
    private readonly IAuthService _sut;

    public AuthServiceTests()
    {
        userManager = new Mock<UserManager<User>>(_userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        _sut = new AuthService(_authenticationOptionsMock.Object,
            userManager.Object);
    }

    [Fact]
    public async Task GenerateAuthenticationToken_WhenInvokedWithNullUser_ShouldReturnAFailureResult()
    {
        // Arrange
        User? user = null;

        // Act
        Result<string> result = await _sut.GenerateAuthenticationToken(user!);

        // Assert
        result.Errors.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task GenerateAuthenticationToken_WhenInvokedWithUser_ShouldReturnACorrectTokenString()
    {
        // Arrange
        string email = _faker.Internet.Email();
        User user = new()
        {
            Email = email,
            UserName = email,
            Id = Guid.NewGuid().ToString()
        };

        AuthenticationOptions options = new()
        {
            Secret = Guid.NewGuid().ToString(),
            ExpirationInMinutes = 10
        };

        _authenticationOptionsMock
            .Setup(x => x.Value)
            .Returns(options);

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(options.Secret));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityTokenHandler tokenHandler = new();
        TokenValidationParameters tokenParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            IssuerSigningKey = signingCredentials.Key
        };

        // Act
        Result<string> result = await _sut.GenerateAuthenticationToken(user);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNullOrEmpty();
        tokenHandler.ValidateToken(result.Value, tokenParameters, out SecurityToken? securityToken);
        securityToken.ShouldNotBeNull();
    }
}