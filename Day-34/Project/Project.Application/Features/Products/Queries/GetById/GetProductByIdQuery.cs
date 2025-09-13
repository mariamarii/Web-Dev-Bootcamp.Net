using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Products.Dtos;

namespace Project.Application.Features.Products.Queries.GetById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;