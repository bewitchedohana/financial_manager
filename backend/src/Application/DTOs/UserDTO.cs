namespace Application.DTOs;

public sealed record UserDTO(
    Guid? Id,
    string? FirstName,
    string? LastName,
    string? Email
);