using Application.Abstractions;
using Application.DTOs;
using Application.Results;
using Domain.Users;

namespace Application.Users;

public sealed class CreateUserUseCase : UseCase<CreateUserDTO, User>
{

    public CreateUserUseCase( )
    {  }

    public override Task<Result<User>> Execute(CreateUserDTO request)
    {
        if (string.IsNullOrEmpty(request.Password))
        {
            return Task.FromResult(Result.Failure<User>("Password is required"));
        }

        if (request.Password.Trim().Length < 16)
        {
            return Task.FromResult(Result.Failure<User>("Password must be at least 16 characters"));
        }

        User user = User.Create(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

        Result<User> result = Result.Success(user);

        return Task.FromResult(result);
    }
}