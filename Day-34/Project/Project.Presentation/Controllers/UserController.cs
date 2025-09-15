using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Users.Commands.Add;
using Project.Application.Features.Users.Commands.Delete;
using Project.Application.Features.Users.Commands.Update;
using Project.Application.Features.Users.Dtos;
using Project.Application.Features.Users.Queries.GetAll;
using Project.Application.Features.Users.Queries.GetById;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class UserController : BaseController
{
    [HttpPost(Router.UserRouter.Add)]
    public async Task<IActionResult> Create(AddUserCommand userCommand)
    {
        var result = await mediator.Send(userCommand);
        return Result(result);
    }

    [HttpDelete(Router.UserRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteUserCommand(id));
        return Result(result);
    }

    [HttpGet(Router.UserRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery request)
    {
        var result = await mediator.Send(request);
        return Result(result);
    }

    [HttpGet(Router.UserRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id));
        return Result(result);
    }

    [HttpPut(Router.UserRouter.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UserWriteDto userWriteDto)
    {
        var command = new UpdateUserCommand(id, userWriteDto.FirstName, userWriteDto.LastName, userWriteDto.Email, userWriteDto.PhoneNumber);
        var result = await mediator.Send(command);
        return Result(result);
    }
}