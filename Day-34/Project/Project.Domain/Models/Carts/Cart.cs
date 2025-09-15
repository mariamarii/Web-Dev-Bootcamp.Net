using Project.Domain.Models.Base;

namespace Project.Domain.Models.Carts;

public class Cart : Entity, IAuditableEntity, ISoftDeletableEntity
{
    public Guid UserId { get; set; }
    public virtual List<CartItem> CartItems { get; set; } = new();
}

