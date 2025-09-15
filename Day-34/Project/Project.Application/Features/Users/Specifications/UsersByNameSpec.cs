using Ardalis.Specification;
using Project.Domain.Models.Users;

namespace Project.Application.Features.Users.Specifications;

public class UsersByNameSpec : Specification<User>
{
    public UsersByNameSpec(string name)
    {
        Query.Where(x => x.FirstName.ToLower().Contains(name.ToLower()) ||
                         x.LastName.ToLower().Contains(name.ToLower()));
    }
}