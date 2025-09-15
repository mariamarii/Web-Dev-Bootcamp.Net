using FluentValidation;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Categories;

namespace Project.Application.Features.Categories.Queries.GetById;

public class GetByIdQueryValidator :AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator(IReadRepository<Category>categoryReadRepository)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .Custom(
                (id, context) =>
                {
                    var category = categoryReadRepository.GetByIdAsync(id).Result;
                    if (category == null)
                    {
                        context.AddFailure("Category not found");
                    }
                }
            )
            ;
    }
}