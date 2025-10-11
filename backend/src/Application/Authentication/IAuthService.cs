using Application.Results;
using Domain.Users;

namespace Application.Authentication;

public interface IAuthService
{
    public Task<Result<User>> Login(string email, string password);
    public Task<Result<string>> GenerateAuthenticationToken(User user);
}