using FluentValidation;
using Project.Domain.Models.Categories;

namespace Project.Application.Features.Categories.Commands.Add;

public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(CategoryConstants.CategoryNameMaxLengthValue).WithMessage(CategoryConstants.CategoryNameMaxLengthMessage);
    }
}