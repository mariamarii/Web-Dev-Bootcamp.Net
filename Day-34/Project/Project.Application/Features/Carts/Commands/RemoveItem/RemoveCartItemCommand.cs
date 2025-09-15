using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Carts.Commands.RemoveItem;

public record RemoveCartItemCommand(Guid CartItemId) : ICommand<string>;