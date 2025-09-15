using FluentValidation;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Categories;
using Project.Domain.Models.Products;

namespace Project.Application.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IReadRepository<Category>categoryRepository, IReadRepository<Product> productRepository)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Product ID is required.")
            .Custom( (id, context) =>
            {
                var product = productRepository.GetByIdAsync(id).Result;
                if (product == null)
                {
                    context.AddFailure("Product not found");
                }
            });
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.")
            .Custom( (categoryId, context) =>
            {
                var category = categoryRepository.GetByIdAsync(categoryId).Result;
                if (category == null)
                {
                    context.AddFailure("Category not found");
                }
            });
    }
    
}