using Ardalis.Specification;
using Project.Domain.Models;
using Project.Domain.Models.Products;

namespace Project.Application.Features.Products.Specifications;

public class ProductsSpec : Specification<Product>
{
    public ProductsSpec(string? name, int pageSize, int pageNumber)
    {
        if (name != null)
            Query.Where(x=>x.Name.Contains(name));
        Query.Skip(pageSize * (pageNumber - 1));
        Query.Take(pageSize);
    }
}