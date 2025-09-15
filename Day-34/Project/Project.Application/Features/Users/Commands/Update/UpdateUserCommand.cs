using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Users.Commands.Update;

public record UpdateUserCommand(Guid Id, string FirstName, string LastName, string Email, string? PhoneNumber) : ICommand<Guid>;