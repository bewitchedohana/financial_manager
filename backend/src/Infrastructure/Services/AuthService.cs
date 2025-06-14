using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Authentication;
using Application.Results;
using Domain.Users;
using Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IOptions<AuthenticationOptions> _authenticationOptions;

    public AuthService(
        IOptions<AuthenticationOptions> authenticationOptions,
        UserManager<User> userManager
    )
    {
        _authenticationOptions = authenticationOptions;
        _userManager = userManager;
    }

    public async Task<Result<string>> GenerateAuthenticationToken(User user)
    {
        if (user is null)
        {
            return Result.Failure<string>("User is null");
        }

        ClaimsIdentity claims = new([
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        ]);

        IList<string> userRoles = await _userManager.GetRolesAsync(user) ?? [];
        foreach (string role in userRoles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_authenticationOptions.Value.Secret));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _authenticationOptions.Value.Issuer,
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(_authenticationOptions.Value.ExpirationInMinutes),
            SigningCredentials = signingCredentials,
            Audience = _authenticationOptions.Value.Audience
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return Result.Success(tokenString);
    }

    public async Task<Result<User>> Login(string email, string password)
    {
        User? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result.Failure<User>("Wrong email or password");
        }

        bool result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
        {
            return Result.Failure<User>("Wrong email or password");
        }

        return Result.Success(user);
    }
}