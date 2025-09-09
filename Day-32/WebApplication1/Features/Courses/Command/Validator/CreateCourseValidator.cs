using FluentValidation;
using WebApplication1.Features.Courses.Command.Models;

namespace WebApplication1.Features.Courses.Command.Validator;

public class CreateCourseValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Course code is required");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Course title is required");
        RuleFor(x => x.Hours).GreaterThan(0).WithMessage("Course hours must be greater than 0");
    }
}
