using Application.DTOs;
using Application.Results;
using Domain.Users;

namespace Application.Users;

public interface IUserService
{
    Task<Result<UserDTO>> CreateUser(User user, string password);
}