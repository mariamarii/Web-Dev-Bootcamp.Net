using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models;
using Project.Domain.Models.Products;
using Project.Domain.Responses;

namespace Project.Application.Features.Products.Commands.Add;

public class AddProductCommandHandler(IMapper mapper, IRepository<Product> productRepository) : ICommandHandler<AddProductCommand, string>
{
    public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);
        await productRepository.AddAsync(product, cancellationToken);
        return Response<string>.Success();
    }
}