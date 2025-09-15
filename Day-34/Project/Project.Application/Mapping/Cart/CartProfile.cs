using AutoMapper;
using Project.Application.Features.Carts.Commands.Add;
using Project.Application.Features.Carts.Commands.AddItem;
using Project.Application.Features.Carts.Commands.UpdateItem;
using Project.Application.Features.Carts.Dtos;
using Project.Domain.Models.Carts;

namespace Project.Application.Mapping.Cart;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<AddCartCommand, Domain.Models.Carts.Cart>();
        CreateMap<Domain.Models.Carts.Cart, CartDto>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src =>
                src.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice)));

        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));

        CreateMap<AddCartItemCommand, CartItem>();
        CreateMap<UpdateCartItemCommand, CartItem>();
    }
}