using Project.Application.Abstractions.Messaging;
using Project.Domain.Responses;

namespace Project.Application.Features.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : ICommand<Guid>;