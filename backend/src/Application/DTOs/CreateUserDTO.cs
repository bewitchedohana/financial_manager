namespace Application.DTOs;

public sealed record CreateUserDTO(
    string FirstName,
    string LastName,
    string Email,
    string Password
);