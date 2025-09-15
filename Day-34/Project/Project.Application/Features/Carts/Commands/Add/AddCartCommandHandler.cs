using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Carts.Dtos;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.Add;

public class AddCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository)
    : ICommandHandler<AddCartCommand, CartDto>
{
    public async Task<Response<CartDto>> Handle(AddCartCommand request, CancellationToken cancellationToken)
    {
        var cart = new Cart
        {
            UserId = request.UserId,
            CartItems = new List<CartItem>()
        };

        await cartRepository.AddAsync(cart, cancellationToken);

        var cartDto = mapper.Map<CartDto>(cart);
        return Response<CartDto>.Success(cartDto);
    }
}