using FluentValidation;
using Project.Domain.Models.Carts;

namespace Project.Application.Features.Carts.Commands.AddItem;

public class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(CartConstants.MinQuantityPerItem)
            .LessThanOrEqualTo(CartConstants.MaxQuantityPerItem)
            .WithMessage($"Quantity must be between {CartConstants.MinQuantityPerItem} and {CartConstants.MaxQuantityPerItem}.");
    }
}