using Ardalis.Specification;
using Project.Domain.Models.Products;

namespace Project.Application.Features.Products.Specifications;

public class ProductsByCategorySpec : Specification<Product>
{
    public ProductsByCategorySpec(Guid categoryId)
    {
        Query.Where(x => x.CategoryId == categoryId);
    }
}

