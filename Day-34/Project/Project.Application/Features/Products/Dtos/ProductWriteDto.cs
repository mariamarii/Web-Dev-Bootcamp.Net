namespace Project.Application.Features.Products.Dtos;

public record ProductWriteDto(string Name, decimal Price, Guid CategoryId);
