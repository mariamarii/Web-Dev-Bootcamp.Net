using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Repositories.Interfaces;

namespace WebApplication3.Repositories.Implementations;

public class CartRepository(ApplicationDbContext context) : GenericRepository<Cart>(context), ICartRepository
{
    public async Task<Cart?> GetCartWithItemsAndProductsAsync(string userId)
    {
        return await context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}
