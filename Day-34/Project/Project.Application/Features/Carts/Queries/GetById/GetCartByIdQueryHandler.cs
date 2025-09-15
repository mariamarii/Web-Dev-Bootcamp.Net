using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Carts.Dtos;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Queries.GetById;

public class GetCartByIdQueryHandler(IMapper mapper, IRepository<Cart> cartRepository)
    : IQueryHandler<GetCartByIdQuery, CartDto>
{
    public async Task<Response<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart == null)
            return Response<CartDto>.Failure("Cart not found");

        var cartDto = mapper.Map<CartDto>(cart);
        return Response<CartDto>.Success(cartDto);
    }
}