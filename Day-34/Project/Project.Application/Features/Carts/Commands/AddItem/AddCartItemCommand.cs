using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Carts.Commands.AddItem;

public record AddCartItemCommand(Guid CartId, Guid ProductId, int Quantity) : ICommand<string>;