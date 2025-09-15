using Project.Application.Features.Products.Dtos;


namespace Project.Application.Features.Categories.Dtos;

public record CategoryWithProductsDto(Guid Id, string Name, List<ProductDto> Products);

