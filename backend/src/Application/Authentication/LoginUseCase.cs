using Application.Abstractions;
using Application.DTOs;
using Application.Results;

namespace Application.Authentication;

public sealed class LoginUseCase : UseCase<LoginRequestDTO, string>
{
	private readonly IAuthService _authService;

	public LoginUseCase(IAuthService authService)
	{
		_authService = authService;
	}

    public override async Task<Result<string>> Execute(LoginRequestDTO request)
    {
        Result<Domain.Users.User> user = await _authService.Login(request.Email, request.Password);
		if (!user.IsSuccess)
		{
			return Result.Failure<string>("Wrong email or password");
		}

        Result<string> token = await _authService.GenerateAuthenticationToken(user.Value!);

		return token;
    }
}
