using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Products.Commands.Update;

public record UpdateProductCommand(Guid Id, string Name, decimal Price, Guid CategoryId) : ICommand<Guid>;
