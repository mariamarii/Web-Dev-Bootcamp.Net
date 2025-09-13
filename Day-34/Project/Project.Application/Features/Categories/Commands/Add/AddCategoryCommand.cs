using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Categories.Commands.Add;

public record AddCategoryCommand(string Name) : ICommand<Guid>;