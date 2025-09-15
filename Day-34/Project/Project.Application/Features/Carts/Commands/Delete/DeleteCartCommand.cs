using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Carts.Commands.Delete;

public record DeleteCartCommand(Guid CartId) : ICommand<string>;