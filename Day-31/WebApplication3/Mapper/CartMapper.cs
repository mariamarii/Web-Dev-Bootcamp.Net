using AutoMapper;
using WebApplication3.Dtos.Cart;
using WebApplication3.Models;

namespace WebApplication3.Mapper;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<Cart, CartDto>();
        
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
    }
}
