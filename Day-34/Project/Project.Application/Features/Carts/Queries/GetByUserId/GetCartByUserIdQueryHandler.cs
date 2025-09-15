using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Carts.Dtos;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Queries.GetByUserId;

public class GetCartByUserIdQueryHandler(IMapper mapper, IRepository<Cart> cartRepository)
    : IQueryHandler<GetCartByUserIdQuery, CartDto>
{
    public async Task<Response<CartDto>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(cancellationToken);

        if (cart == null)
            return Response<CartDto>.Failure("Cart not found for this user");

        var cartDto = mapper.Map<CartDto>(cart);
        return Response<CartDto>.Success(cartDto);
    }
}