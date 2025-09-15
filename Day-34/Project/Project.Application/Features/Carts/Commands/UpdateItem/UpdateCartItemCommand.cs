using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Carts.Commands.UpdateItem;

public record UpdateCartItemCommand(Guid CartItemId, int Quantity) : ICommand<string>;