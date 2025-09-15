namespace Project.Application.Features.Users.Dtos;

public record UserDto(Guid Id, string FirstName, string LastName, string Email, string? PhoneNumber);