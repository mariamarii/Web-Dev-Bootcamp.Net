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
            .ConstructUsing(src => new CartDto(
                src.Id,
                src.UserId,
                src.CartItems.Select(ci => new CartItemDto(
                    ci.Id,
                    ci.Quantity,
                    ci.UnitPrice,
                    ci.ProductId,
                    ci.Product != null ? ci.Product.Name : string.Empty,
                    ci.Quantity * ci.UnitPrice
                )).ToList(),
                src.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice)
            ));

        CreateMap<CartItem, CartItemDto>()
            .ConstructUsing(src => new CartItemDto(
                src.Id,
                src.Quantity,
                src.UnitPrice,
                src.ProductId,
                src.Product != null ? src.Product.Name : string.Empty,
                src.Quantity * src.UnitPrice
            ));

        CreateMap<AddCartItemCommand, CartItem>();
        CreateMap<UpdateCartItemCommand, CartItem>();
    }
}