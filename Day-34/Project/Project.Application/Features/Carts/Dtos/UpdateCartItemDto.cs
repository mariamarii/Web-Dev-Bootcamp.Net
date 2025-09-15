namespace Project.Application.Features.Carts.Dtos;

public record UpdateCartItemDto(Guid CartItemId, int Quantity);