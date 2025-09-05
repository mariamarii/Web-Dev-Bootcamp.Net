namespace WebApplication3.Dtos.Cart;

public class CartDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
}
