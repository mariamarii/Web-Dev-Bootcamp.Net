using FluentValidation;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Categories;

namespace Project.Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    
    public UpdateCategoryCommandValidator(IReadRepository<Category>categoryRepository)
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(CategoryConstants.CategoryNameMaxLengthValue).WithMessage(CategoryConstants.CategoryNameMaxLengthMessage);

        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Category ID is required.")
            .Custom(
                (id, context) =>
                {
                    var category = categoryRepository.GetByIdAsync(id).Result;
                    if (category == null)
                    {
                        context.AddFailure("Category not found");
                    }
                }
            );

    }
    
}