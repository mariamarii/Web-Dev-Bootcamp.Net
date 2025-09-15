using Project.Domain.Models.Base;
using Project.Domain.Models.Users;

namespace Project.Domain.Models.Carts;

public class Cart : Entity, IAuditableEntity, ISoftDeletableEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual List<CartItem> CartItems { get; set; } = new();
}

