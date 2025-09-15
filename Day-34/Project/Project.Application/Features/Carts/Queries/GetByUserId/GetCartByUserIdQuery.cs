using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Carts.Dtos;

namespace Project.Application.Features.Carts.Queries.GetByUserId;

public record GetCartByUserIdQuery(Guid UserId) : IQuery<CartDto>;