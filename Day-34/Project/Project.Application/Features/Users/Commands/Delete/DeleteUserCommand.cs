using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Users.Commands.Delete;

public record DeleteUserCommand(Guid Id) : ICommand<string>;