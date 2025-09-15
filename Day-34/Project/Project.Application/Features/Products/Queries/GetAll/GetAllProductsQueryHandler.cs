using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.Specifications;
using Project.Domain.Models;
using Project.Domain.Models.Products;
using Project.Domain.Responses;

namespace Project.Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryHandler(IMapper mapper, IReadRepository<Product> productRepository) : IQueryHandler<GetAllProductsQuery, PaginatedResult<ProductDto>>
{
    public async Task<Response<PaginatedResult<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository
            .ListAsync(new ProductsSpec(request.Name, 
                request.PageSize, 
                request.PageNumber), cancellationToken);

        var productsCount = await productRepository
            .CountAsync(new ProductsSpec(request.Name,
                request.PageSize,
                request.PageNumber), cancellationToken);
        
        var mappedProducts = mapper.Map<IEnumerable<ProductDto>>(products);
        
        return Response<ProductDto>.GetData(mappedProducts ,request.PageNumber, request.PageSize, productsCount);
    }
}