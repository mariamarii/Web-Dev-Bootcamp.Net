using Project.Domain.Models.Base;
using Project.Domain.Models.Categories;

namespace Project.Domain.Models.Products
{
    public class Product : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
