using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Carts.Dtos;

namespace Project.Application.Features.Carts.Commands.Add;

public record AddCartCommand(Guid UserId) : ICommand<CartDto>;