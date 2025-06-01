using Application.Results;
using Domain.Users;

namespace Application.Authentication;

public interface IAuthService
{
    public Task<Result<string>> GenerateAuthenticationToken(User user);
}