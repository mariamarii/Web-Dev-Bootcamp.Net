using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Products.Dtos;
using Project.Domain.Models;
using Project.Domain.Models.Products;
using Project.Domain.Responses;

namespace Project.Application.Features.Products.Queries.GetById;

public class GetProductByIdQueryHandler(IMapper mapper, IReadRepository<Product> productRepository) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Response<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
        {
            return Response<ProductDto>.NotFound("Products not found");
        }

        var productDto = mapper.Map<ProductDto>(product);
        return Response<ProductDto>.Success(productDto);
    }
}