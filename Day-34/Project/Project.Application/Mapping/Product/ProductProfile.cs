using AutoMapper;
using Project.Application.Features.Products.Commands.Add;
using Project.Application.Features.Products.Commands.Update;
using Project.Application.Features.Products.Dtos;

namespace Project.Application.Mapping.Product;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductCommand, Domain.Models.Products.Product>();
        CreateMap<Domain.Models.Products.Product, ProductDto>();
        CreateMap<Domain.Models.Products.Product, UpdateProductCommand>().ReverseMap();
    }
}