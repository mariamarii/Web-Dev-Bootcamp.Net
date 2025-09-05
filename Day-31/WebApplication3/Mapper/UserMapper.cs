using AutoMapper;
using WebApplication3.Dtos;
using WebApplication3.Dtos.Product;
using WebApplication3.Dtos.Cart;
using WebApplication3.Models;

namespace WebApplication3.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        // User mappings
        CreateMap<RegisterModal, User>();
        
        // Product mappings
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        
        // Cart mappings
        CreateMap<Cart, CartDto>();
        
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
    }
}
