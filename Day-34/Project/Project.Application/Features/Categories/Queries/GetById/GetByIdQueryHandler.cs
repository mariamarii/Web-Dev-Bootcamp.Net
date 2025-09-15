using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Specifications;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.Specifications;
using Project.Domain.Models.Categories;
using Project.Domain.Models.Products;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Queries.GetById;

public class GetByIdQueryHandler (IMapper mapper,IReadRepository<Category> categoryRepository, IReadRepository<Product> productRepository) : IQueryHandler<GetByIdQuery, CategoryWithProductsDto>
{
    public async Task<Response<CategoryWithProductsDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
       

        var products = await productRepository.ListAsync(new ProductsByCategorySpec(request.Id), cancellationToken);
        var mappedProducts = mapper.Map<List<ProductDto>>(products);
        var mappedCategory = new CategoryWithProductsDto(category.Id, category.Name, mappedProducts);
        return Response<CategoryWithProductsDto>.Success(mappedCategory);
    }

    
}