using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Products.Commands.Add;
using Project.Application.Features.Products.Commands.Delete;
using Project.Application.Features.Products.Commands.Update;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.Queries;
using Project.Application.Features.Products.Queries.GetAll;
using Project.Application.Features.Products.Queries.GetById;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class ProductController : BaseController
{
    [HttpPost(Router.ProductRouter.Add)]
    public async Task<IActionResult> Create(AddProductCommand productCommand)
    {
        var result = await mediator.Send(productCommand);
        return Result(result);
    }

    [HttpDelete(Router.ProductRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        return Result(result);
    }

    [HttpGet(Router.ProductRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQuery request)
    {
        var result = await mediator.Send(request);
        return Result(result);
    }

    [HttpGet(Router.ProductRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id));
        return Result(result);
    }

    //update
    [HttpPut(Router.ProductRouter.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, ProductWriteDto ProductWriteDto)
    {
        var command = new UpdateProductCommand(id, ProductWriteDto.Name, ProductWriteDto.Price, ProductWriteDto.CategoryId);
        var result = await mediator.Send(command);
        return Result(result);
    }

}