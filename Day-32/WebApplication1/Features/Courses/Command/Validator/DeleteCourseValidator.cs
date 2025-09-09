using FluentValidation;
using WebApplication1.Features.Courses.Command.Models;

namespace WebApplication1.Features.Courses.Command.Validator;

public class DeleteCourseValidator : AbstractValidator<DeleteCourseDto>
{
    public DeleteCourseValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
    }
}
