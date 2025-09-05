using WebApplication3.Dtos.Cart;
using WebApplication3.Models;

namespace WebApplication3.Services.Interfaces;

public interface ICartService
{
    Task<Response<CartDto>> AddToCartAsync(AddToCartDto addToCartDto, string userId);
    Task<Response<CartDto>> GetCartAsync(string userId);
    Task<Response<object>> RemoveFromCartAsync(int itemId, string userId);
}
