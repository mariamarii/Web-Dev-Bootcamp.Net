using FluentValidation;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Categories;

namespace Project.Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<Category>
{
    public DeleteCategoryCommandValidator(IReadRepository<Category>categoryRepository)
    {
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