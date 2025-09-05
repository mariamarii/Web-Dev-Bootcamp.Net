namespace WebApplication3.Models;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
    public int Quantity { get; set; }
}