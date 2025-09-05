using WebApplication3.Enum;

namespace WebApplication3.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!; 
    public string CreatedBy { get; set; } = null!; 
    public ProductStatus Status { get; set; } = ProductStatus.Pending;
}