using Project.Domain.Models.Base;
using Project.Domain.Models.Products;

namespace Project.Domain.Models.Categories
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
