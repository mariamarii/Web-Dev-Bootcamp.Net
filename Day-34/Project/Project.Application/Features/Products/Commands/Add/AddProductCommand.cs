using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Products.Commands.Add;

public record AddProductCommand(string Name, decimal Price, Guid CategoryId) : ICommand<string>;