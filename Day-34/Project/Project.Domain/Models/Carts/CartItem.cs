using Project.Domain.Models.Base;
using Project.Domain.Models.Products;

namespace Project.Domain.Models.Carts;

public class CartItem : Entity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; } = null!;

    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
}