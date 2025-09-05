namespace WebApplication3.Dtos.Product;

public class ProductCreateDto
{
    
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public IFormFile Image { get; set; } = null!; 
    

}