using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Products;
using Project.Domain.Responses;

namespace Project.Application.Features.Products.Commands.Delete;

public class DeleteProductCommandHandler(IRepository<Product> productRepository) : ICommandHandler<DeleteProductCommand, Guid>
{
    public async Task<Response<Guid>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return Response<Guid>.NotFound("Product not found.");
        }

        await productRepository.DeleteAsync(product, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}