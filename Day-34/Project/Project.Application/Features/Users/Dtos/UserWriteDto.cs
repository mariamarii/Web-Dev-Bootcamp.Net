namespace Project.Application.Features.Users.Dtos;

public record UserWriteDto(string FirstName, string LastName, string Email, string? PhoneNumber);