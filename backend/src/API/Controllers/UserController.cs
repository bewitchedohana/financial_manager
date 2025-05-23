using Application.DTOs;
using Application.Results;
using Application.Users;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDTO createUserDTO)
    {
        CreateUserUseCase useCase = new();
        Result<User> user = await useCase.Execute(createUserDTO);
        Result<UserDTO> response = await _userService.CreateUser(user.Value!, createUserDTO.Password);

        return Ok(response);
    }
}