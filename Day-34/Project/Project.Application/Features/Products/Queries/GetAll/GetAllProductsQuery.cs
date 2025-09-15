using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Products.Dtos;
using Project.Domain.Filters;
using Project.Domain.Responses;

namespace Project.Application.Features.Products.Queries.GetAll;

public record GetAllProductsQuery(string? Name): BaseFilter, IQuery<PaginatedResult<ProductDto>>;