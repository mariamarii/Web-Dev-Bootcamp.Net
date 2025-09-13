using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Categories.Commands.Add;
using Project.Application.Features.Categories.Commands.Delete;
using Project.Application.Features.Categories.Commands.Update;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Queries.GetAll;
using Project.Application.Features.Categories.Queries.GetById;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class CategoryController : BaseController
{
    [HttpPost(Router.CategoryRouter.Add)]
    public async Task<IActionResult> Create(AddCategoryCommand productCommand)
    {
        var result = await mediator.Send(productCommand);
        return Result(result);
    }

    [HttpGet(Router.CategoryRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoriesQuery getAllCategoriesQuery)
    {
        var result = await mediator.Send(getAllCategoriesQuery);
        return Result(result);
    }

    [HttpGet(Router.CategoryRouter.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetByIdQuery(id));
        return Result(result);
    }

    [HttpPut(Router.CategoryRouter.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id, CategoryWriteDto CategoryWriteDto)
    {
        var command = new UpdateCategoryCommand(id, CategoryWriteDto.Name);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpDelete(Router.CategoryRouter.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id));
        return Result(result);
    }
}