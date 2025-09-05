namespace WebApplication3.Dtos.Cart;

public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductImageUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}