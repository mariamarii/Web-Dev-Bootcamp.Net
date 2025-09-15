namespace Project.Application.Features.Carts.Dtos;

public record AddCartItemDto(Guid ProductId, int Quantity);