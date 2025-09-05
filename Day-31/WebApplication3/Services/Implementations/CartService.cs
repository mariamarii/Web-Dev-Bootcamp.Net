using AutoMapper;
using System.Net;
using WebApplication3.Dtos.Cart;
using WebApplication3.Enum;
using WebApplication3.Models;
using WebApplication3.Repositories.Interfaces;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Services.Implementations;

public class CartService(
    ICartRepository cartRepository,
    IGenericRepository<CartItem> cartItemRepository,
    IGenericRepository<Product> productRepository,
    IMapper mapper) : ICartService
{

    public async Task<Response<CartDto>> AddToCartAsync(AddToCartDto addToCartDto, string userId)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(addToCartDto.ProductId);
            if (product == null || product.Status != ProductStatus.Approved)
            {
                return new Response<CartDto>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Product not found or not available"
                };
            }

            var cart = await cartRepository.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await cartRepository.AddAsync(cart);
                await cartRepository.SaveChangesAsync();
            }

            var existingItem = await cartItemRepository.FirstOrDefaultAsync(
                ci => ci.CartId == cart.Id && ci.ProductId == addToCartDto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += addToCartDto.Quantity;
                cartItemRepository.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity
                };
                await cartItemRepository.AddAsync(cartItem);
            }

            await cartItemRepository.SaveChangesAsync();

            var updatedCart = await GetCartAsync(userId);
            return updatedCart;
        }
        catch (Exception ex)
        {
            return new Response<CartDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<CartDto>> GetCartAsync(string userId)
    {
        try
        {
            var cart = await cartRepository.GetCartWithItemsAndProductsAsync(userId);
            if (cart == null)
            {
                return new Response<CartDto>
                {
                    Status = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Cart is empty",
                    Data = new CartDto { UserId = userId, Items = new List<CartItemDto>() }
                };
            }

            var cartDto = mapper.Map<CartDto>(cart);

            return new Response<CartDto>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Cart retrieved successfully",
                Data = cartDto
            };
        }
        catch (Exception ex)
        {
            return new Response<CartDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<object>> RemoveFromCartAsync(int itemId, string userId)
    {
        try
        {
            var cartItem = await cartItemRepository.GetByIdAsync(itemId);
            if (cartItem == null)
            {
                return new Response<object>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Cart item not found"
                };
            }

            var cart = await cartRepository.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == cartItem.CartId);
            if (cart == null)
            {
                return new Response<object>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = "Access denied"
                };
            }

            cartItemRepository.Remove(cartItem);
            await cartItemRepository.SaveChangesAsync();

            return new Response<object>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Item removed from cart successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<object>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}

