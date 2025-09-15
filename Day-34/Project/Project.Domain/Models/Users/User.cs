using Project.Domain.Models.Base;
using Project.Domain.Models.Carts;

namespace Project.Domain.Models.Users;

public class User : Entity, IAuditableEntity, ISoftDeletableEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    
    // Navigation properties
    public virtual List<Cart> Carts { get; set; } = new();
}