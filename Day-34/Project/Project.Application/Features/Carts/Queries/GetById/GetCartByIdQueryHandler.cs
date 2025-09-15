using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Carts.Dtos;
using Project.Application.Features.Carts.Specifications;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Queries.GetById;

public class GetCartByIdQueryHandler(IMapper mapper, IRepository<Cart> cartRepository)
    : IQueryHandler<GetCartByIdQuery, CartDto>
{
    public async Task<Response<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new CartWithItemsSpec(request.Id);
        var cart = await cartRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (cart == null)
            return Response<CartDto>.Failure("Cart not found");

        var cartDto = mapper.Map<CartDto>(cart);
        return Response<CartDto>.Success(cartDto);
    }
}