using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Carts.Commands.Add;
using Project.Application.Features.Carts.Commands.AddItem;
using Project.Application.Features.Carts.Commands.Delete;
using Project.Application.Features.Carts.Commands.RemoveItem;
using Project.Application.Features.Carts.Commands.UpdateItem;
using Project.Application.Features.Carts.Dtos;
using Project.Application.Features.Carts.Queries.GetById;
using Project.Application.Features.Carts.Queries.GetByUserId;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class CartController : BaseController
{
    [HttpPost(Router.CartRouter.Add)]
    public async Task<IActionResult> Create(AddCartCommand cartCommand)
    {
        var result = await mediator.Send(cartCommand);
        return Result(result);
    }

    [HttpDelete(Router.CartRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCartCommand(id));
        return Result(result);
    }

    [HttpGet(Router.CartRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCartByIdQuery(id));
        return Result(result);
    }

    [HttpGet(Router.CartRouter.GetByUserId)]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var result = await mediator.Send(new GetCartByUserIdQuery(userId));
        return Result(result);
    }

    [HttpPost(Router.CartRouter.AddItem)]
    public async Task<IActionResult> AddItem(Guid cartId, AddCartItemDto addCartItemDto)
    {
        var command = new AddCartItemCommand(cartId, addCartItemDto.ProductId, addCartItemDto.Quantity);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpPut(Router.CartRouter.UpdateItem)]
    public async Task<IActionResult> UpdateItem(Guid cartItemId, UpdateCartItemDto updateCartItemDto)
    {
        var command = new UpdateCartItemCommand(cartItemId, updateCartItemDto.Quantity);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpDelete(Router.CartRouter.RemoveItem)]
    public async Task<IActionResult> RemoveItem(Guid cartItemId)
    {
        var result = await mediator.Send(new RemoveCartItemCommand(cartItemId));
        return Result(result);
    }
}