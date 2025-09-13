using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Products.Commands.Add;

public record AddProductCommand(string Name, Guid CategoryId) : ICommand<string>;