using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Controllers.Base;
using WebApplication3.Dtos.Product;
using WebApplication3.Models;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : BaseController
{

    [HttpPost]
    [Authorize(Roles = "ProductCreator")]
    public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto createDto)
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
        var result = await productService.CreateProductAsync(createDto, userId!);
        return Result(result);
    }

    [HttpGet("mine")]
    [Authorize(Roles = "ProductCreator")]
    public async Task<IActionResult> GetMyProducts()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await productService.GetMyProductsAsync(userId!);
        return Result(result);
    }

    [HttpGet("pending")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPendingProducts()
    {
        var result = await productService.GetPendingProductsAsync();
        return Result(result);
    }

    [HttpPut("{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveProduct(int id)
    {
        var result = await productService.ApproveProductAsync(id);
        return Result(result);
    }

    [HttpPut("{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectProduct(int id)
    {
        var result = await productService.RejectProductAsync(id);
        return Result(result);
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetApprovedProducts()
    {
        var result = await productService.GetApprovedProductsAsync();
        return Result(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await productService.GetProductByIdAsync(id);
        return Result(result);
    }
}

