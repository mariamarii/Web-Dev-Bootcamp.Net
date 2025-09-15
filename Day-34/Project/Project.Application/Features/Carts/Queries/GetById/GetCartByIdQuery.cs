using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Carts.Dtos;

namespace Project.Application.Features.Carts.Queries.GetById;

public record GetCartByIdQuery(Guid Id) : IQuery<CartDto>;