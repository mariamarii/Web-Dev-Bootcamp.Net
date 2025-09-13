using FluentValidation;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models;
using Project.Domain.Models.Categories;
using Project.Domain.Models.Products;

namespace Project.Application.Features.Products.Commands.Add;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    private readonly IReadRepository<Category> _categoryRepository;
    public AddProductValidator(IReadRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Products name is required.")
            .MaximumLength(ProductConstants.ProductNameMaxLengthValue).WithMessage(ProductConstants.ProductNameMaxLengthMessage);

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("Categories ID is required.");
        // .Custom(async void (categoryId, context) =>
        // {
        //     try
        //     {
        //         var category = await _categoryRepository.GetByIdAsync(categoryId);
        //         if (category == null)
        //         {
        //             context.AddFailure("CategoryId", $"Categories with ID {categoryId} does not exist.");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception(e.Message);
        //     }
        // });


    }
}