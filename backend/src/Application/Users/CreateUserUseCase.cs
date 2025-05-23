using Application.Abstractions;
using Application.DTOs;
using Application.Results;

namespace Application.Users;

internal sealed class CreateUserUseCase : UseCase<CreateUserDTO, UserDTO>
{
    public CreateUserUseCase() { }

    public override Result<UserDTO> Execute(CreateUserDTO request)
    {
        if (string.IsNullOrEmpty(request.Password))
        {
            return Result.Failure<UserDTO>(new ErrorResult("BAD_REQUEST", "Password is required"));
        }

        if (request.Password.Trim().Length < 16)
        {
            return Result.Failure<UserDTO>(new ErrorResult("BAD_REQUEST", "Password must be at least 16 characters"));
        }

        UserDTO response = new(Guid.NewGuid(), string.Empty, string.Empty, string.Empty);
        return Result.Success(response);
    }
}