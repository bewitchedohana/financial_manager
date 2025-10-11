namespace Application.DTOs;

public sealed record UserDTO(
    string Id,
    string? FirstName,
    string? LastName,
    string? Email
);