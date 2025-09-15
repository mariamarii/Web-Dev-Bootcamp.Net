using Ardalis.Specification;
using Project.Domain.Models.Categories;

namespace Project.Application.Features.Categories.Specifications;

public class Categoryspec : Specification<Category>
{
    public Categoryspec(string? name, int pageSize, int pageNumber)
    {
        if (name != null)
            Query.Where(x => x.Name.Contains(name));
        Query.Skip(pageSize * (pageNumber - 1));
        Query.Take(pageSize);
    }
    
}