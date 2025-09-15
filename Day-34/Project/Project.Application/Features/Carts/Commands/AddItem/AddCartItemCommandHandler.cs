using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Carts;
using Project.Domain.Models.Products;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.AddItem;

public class AddCartItemCommandHandler(
    IRepository<Cart> cartRepository,
    IRepository<Product> productRepository,
    IRepository<CartItem> cartItemRepository)
    : ICommandHandler<AddCartItemCommand, string>
{
    public async Task<Response<string>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.CartId, cancellationToken);
        if (cart == null)
            return Response<string>.Failure("Cart not found");

        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
            return Response<string>.Failure("Product not found");

        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
            await cartItemRepository.UpdateAsync(existingItem, cancellationToken);
        }
        else
        {
            var cartItem = new CartItem
            {
                CartId = request.CartId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = product.Price
            };

            await cartItemRepository.AddAsync(cartItem, cancellationToken);
        }

        return Response<string>.Success("Item added to cart successfully");
    }
}