using Ardalis.Specification;
using Project.Domain.Models.Carts;

namespace Project.Application.Features.Carts.Specifications;

public class CartsByUserIdSpec : Specification<Cart>
{
    public CartsByUserIdSpec(Guid userId)
    {
        Query.Where(x => x.UserId == userId)
             .Include(x => x.CartItems)
             .ThenInclude(x => x.Product);
    }
}