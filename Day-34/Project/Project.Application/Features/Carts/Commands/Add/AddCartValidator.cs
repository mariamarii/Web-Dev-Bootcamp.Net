using FluentValidation;

namespace Project.Application.Features.Carts.Commands.Add;

public class AddCartValidator : AbstractValidator<AddCartCommand>
{
    public AddCartValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
}