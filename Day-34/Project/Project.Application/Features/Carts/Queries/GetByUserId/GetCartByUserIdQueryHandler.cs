using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Carts.Dtos;
using Project.Application.Features.Carts.Specifications;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Queries.GetByUserId;

public class GetCartByUserIdQueryHandler(IMapper mapper, IRepository<Cart> cartRepository)
    : IQueryHandler<GetCartByUserIdQuery, CartDto>
{
    public async Task<Response<CartDto>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new CartsByUserIdSpec(request.UserId);
        var cart = await cartRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (cart == null)
            return Response<CartDto>.Failure("Cart not found for this user");

        var cartDto = mapper.Map<CartDto>(cart);
        return Response<CartDto>.Success(cartDto);
    }
}