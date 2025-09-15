namespace Project.Application.Features.Carts.Dtos;

public record CartDto(Guid Id, Guid UserId, List<CartItemDto> CartItems)
{
    public decimal TotalAmount => CartItems.Sum(ci => ci.SubTotal);
}