using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Controllers.Base;
using WebApplication3.Dtos.Cart;
using WebApplication3.Models;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "User")]
public class CartController(ICartService cartService) : BaseController
{

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
    {
        if (!ModelState.IsValid)
        {
            return Result(new Response<object>
            {
                Data = ModelState,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            });
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await cartService.AddToCartAsync(addToCartDto, userId!);
        return Result(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await cartService.GetCartAsync(userId!);
        return Result(result);
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> RemoveFromCart(int itemId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await cartService.RemoveFromCartAsync(itemId, userId!);
        return Result(result);
    }
}

