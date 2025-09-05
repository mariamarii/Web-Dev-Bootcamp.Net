using WebApplication3.Models;

namespace WebApplication3.Repositories.Interfaces;

public interface ICartRepository : IGenericRepository<Cart>
{
    Task<Cart?> GetCartWithItemsAndProductsAsync(string userId);
}
