using Ardalis.Specification;
using Project.Domain.Models.Users;

namespace Project.Application.Features.Users.Specifications;

public class UsersByEmailSpec : Specification<User>
{
    public UsersByEmailSpec(string email)
    {
        Query.Where(x => x.Email.ToLower().Contains(email.ToLower()));
    }
}