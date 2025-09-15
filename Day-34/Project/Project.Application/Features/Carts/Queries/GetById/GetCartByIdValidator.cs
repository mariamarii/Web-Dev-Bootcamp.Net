using FluentValidation;

namespace Project.Application.Features.Carts.Queries.GetById;

public class GetCartByIdValidator : AbstractValidator<GetCartByIdQuery>
{
    public GetCartByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required.");
    }
}