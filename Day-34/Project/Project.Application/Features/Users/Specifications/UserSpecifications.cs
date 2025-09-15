using Project.Domain.Models.Users;
using System.Linq.Expressions;

namespace Project.Application.Features.Users.Specifications;

public static class UserSpecifications
{
    public static Expression<Func<User, bool>> ByEmail(string email)
    {
        return user => user.Email.ToLower().Contains(email.ToLower());
    }
    
    public static Expression<Func<User, bool>> ByName(string name)
    {
        return user => user.FirstName.ToLower().Contains(name.ToLower()) || 
                      user.LastName.ToLower().Contains(name.ToLower());
    }
}