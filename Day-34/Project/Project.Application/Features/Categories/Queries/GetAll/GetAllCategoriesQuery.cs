using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Categories.Dtos;
using Project.Domain.Filters;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Queries.GetAll;

public record GetAllCategoriesQuery(string? Name): BaseFilter, IQuery<PaginatedResult<CategoryDto>>;
