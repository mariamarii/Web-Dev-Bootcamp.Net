namespace Project.Application.Features.Carts.Dtos;

public record CartDto(Guid Id, Guid UserId, List<CartItemDto> CartItems, decimal TotalAmount);