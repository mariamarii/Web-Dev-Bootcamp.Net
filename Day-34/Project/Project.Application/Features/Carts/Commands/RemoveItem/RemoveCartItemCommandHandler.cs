using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.RemoveItem;

public class RemoveCartItemCommandHandler(IRepository<CartItem> cartItemRepository)
    : ICommandHandler<RemoveCartItemCommand, string>
{
    public async Task<Response<string>> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.CartItemId, cancellationToken);
        if (cartItem == null)
            return Response<string>.Failure("Cart item not found");

        await cartItemRepository.DeleteAsync(cartItem, cancellationToken);
        return Response<string>.Success("Cart item removed successfully");
    }
}