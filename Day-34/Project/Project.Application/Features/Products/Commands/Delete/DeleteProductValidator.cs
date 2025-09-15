using FluentValidation;

namespace Project.Application.Features.Products.Commands.Delete;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Product ID is required.");
    }
}