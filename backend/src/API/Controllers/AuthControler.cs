using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Authentication;

namespace API.Controllers;

[Route("api/Auth")]
[ApiController]
public class AuthControler(
    IAuthService authService
    ) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        LoginUseCase loginUseCase = new(_authService);
        var response = await loginUseCase.Execute(request);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
