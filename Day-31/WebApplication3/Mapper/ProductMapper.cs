using AutoMapper;
using WebApplication3.Dtos.Product;
using WebApplication3.Models;

namespace WebApplication3.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
