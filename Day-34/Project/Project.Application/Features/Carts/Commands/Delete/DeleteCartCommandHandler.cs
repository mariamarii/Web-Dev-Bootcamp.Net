using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.Delete;

public class DeleteCartCommandHandler(IRepository<Cart> cartRepository)
    : ICommandHandler<DeleteCartCommand, string>
{
    public async Task<Response<string>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.CartId, cancellationToken);
        if (cart == null)
            return Response<string>.Failure("Cart not found");

        await cartRepository.DeleteAsync(cart, cancellationToken);
        return Response<string>.Success("Cart deleted successfully");
    }
}