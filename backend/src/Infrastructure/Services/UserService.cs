using Application.DTOs;
using Application.Results;
using Application.Users;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public sealed class UserService(UserManager<User> userManager) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<Result<UserDTO>> CreateUser(User user, string password)
    {
        IdentityResult response = await _userManager.CreateAsync(user, password);
        if (!response.Succeeded) {
            return Result.Failure<UserDTO>(string.Join(';',response.Errors.Select(x => x.Description)));
        }

        UserDTO dto = new(user.Id,
            user.FirstName,
            user.LastName,
            user.Email);

        return Result.Success(dto);
    }
}