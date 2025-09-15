using Ardalis.Specification;
using Project.Domain.Models.Carts;

namespace Project.Application.Features.Carts.Specifications;

public class ActiveCartsSpec : Specification<Cart>
{
    public ActiveCartsSpec(int pageSize, int pageNumber)
    {
        Query.Where(x => !x.IsDeleted && x.CartItems.Any())
             .Include(x => x.CartItems)
             .ThenInclude(x => x.Product)
             .Include(x => x.User)
             .Skip(pageSize * (pageNumber - 1))
             .Take(pageSize)
             .OrderByDescending(x => x.UpdatedAt);
    }
}