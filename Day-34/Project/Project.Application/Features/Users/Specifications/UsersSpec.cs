using Ardalis.Specification;
using Project.Domain.Models.Users;

namespace Project.Application.Features.Users.Specifications;

public class UsersSpec : Specification<User>
{
    public UsersSpec(string? searchTerm = null, int pageSize = 10, int pageNumber = 1)
    {
        Query.Where(u => !u.IsDeleted);
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            Query.Where(x => x.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                            x.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                            x.Email.ToLower().Contains(searchTerm.ToLower()));
        }
        
        Query.Skip(pageSize * (pageNumber - 1))
             .Take(pageSize)
             .OrderBy(x => x.FirstName)
             .ThenBy(x => x.LastName);
    }
}