using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Specifications;
using Project.Domain.Models.Categories;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Queries.GetAll;

public class GetAllCategoriesQueryHandler(IMapper mapper,IReadRepository<Category> categoryRepository) : IQueryHandler<GetAllCategoriesQuery, PaginatedResult<CategoryDto>>
{
    public async Task<Response<PaginatedResult<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories =  await categoryRepository
            .ListAsync(new Categoryspec(request.Name,request.PageSize,request.PageNumber), cancellationToken);
        
        var categoriesCount = await  categoryRepository
            .CountAsync(new Categoryspec(request.Name,request.PageSize,request.PageNumber), cancellationToken);
        
         var mappedCategories= mapper.Map<IEnumerable<CategoryDto>>(categories);
        return Response<CategoryDto> .GetData(mappedCategories,request.PageNumber, request.PageSize, categoriesCount);
        
    }
}