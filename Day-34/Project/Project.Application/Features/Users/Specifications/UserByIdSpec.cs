using Ardalis.Specification;
using Project.Domain.Models.Users;

namespace Project.Application.Features.Users.Specifications;

public class UserByIdSpec : Specification<User>
{
    public UserByIdSpec(Guid id)
    {
        Query.Where(u => u.Id == id && !u.IsDeleted);
    }
}