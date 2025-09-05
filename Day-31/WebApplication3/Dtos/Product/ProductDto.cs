namespace WebApplication3.Dtos.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public string Status { get; set; } = null!;
}
