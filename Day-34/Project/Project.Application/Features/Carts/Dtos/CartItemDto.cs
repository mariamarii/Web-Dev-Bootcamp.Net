namespace Project.Application.Features.Carts.Dtos;

public record CartItemDto(Guid Id, int Quantity, decimal UnitPrice, Guid ProductId)
{
    public string ProductName { get; init; } = string.Empty;
    public decimal SubTotal => Quantity * UnitPrice;
}