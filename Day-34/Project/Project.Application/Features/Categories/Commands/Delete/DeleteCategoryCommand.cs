using MediatR;
using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Categories.Commands.Delete;

public record DeleteCategoryCommand(Guid Id) : ICommand<Guid>;
