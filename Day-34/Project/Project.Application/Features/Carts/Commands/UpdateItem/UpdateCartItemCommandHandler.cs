using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.UpdateItem;

public class UpdateCartItemCommandHandler(IRepository<CartItem> cartItemRepository)
    : ICommandHandler<UpdateCartItemCommand, string>
{
    public async Task<Response<string>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.CartItemId, cancellationToken);
        if (cartItem == null)
            return Response<string>.Failure("Cart item not found");

        cartItem.Quantity = request.Quantity;
        await cartItemRepository.UpdateAsync(cartItem, cancellationToken);

        return Response<string>.Success("Cart item updated successfully");
    }
}