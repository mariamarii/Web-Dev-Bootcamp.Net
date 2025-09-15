namespace Project.Application.Features.Carts.Dtos;

public record CartItemDto(Guid Id, int Quantity, decimal UnitPrice, Guid ProductId, string ProductName, decimal SubTotal);