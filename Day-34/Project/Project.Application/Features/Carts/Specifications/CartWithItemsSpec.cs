using Ardalis.Specification;
using Project.Domain.Models.Carts;

namespace Project.Application.Features.Carts.Specifications;

public class CartWithItemsSpec : Specification<Cart>
{
    public CartWithItemsSpec(Guid cartId)
    {
        Query.Where(x => x.Id == cartId)
             .Include(x => x.CartItems)
             .ThenInclude(x => x.Product)
             .Include(x => x.User);
    }
}