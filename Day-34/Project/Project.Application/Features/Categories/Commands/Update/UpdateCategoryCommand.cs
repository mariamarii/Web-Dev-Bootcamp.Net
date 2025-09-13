using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Commands.Update;

public record UpdateCategoryCommand(Guid Id,string Name) : ICommand<Guid>;

