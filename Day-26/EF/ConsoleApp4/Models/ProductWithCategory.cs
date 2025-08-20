namespace ConsoleApp4.Models;

public class ProductWithCategory
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}