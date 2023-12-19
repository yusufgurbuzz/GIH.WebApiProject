namespace GIH.Entities.DTOs;

public record PersonForAuthenticationDto
{
    public string? Username { get; init; }
    public string? Password { get; init; }
}