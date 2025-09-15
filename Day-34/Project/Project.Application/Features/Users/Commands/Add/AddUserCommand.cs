using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Users.Commands.Add;

public record AddUserCommand(string FirstName, string LastName, string Email, string? PhoneNumber) : ICommand<string>;