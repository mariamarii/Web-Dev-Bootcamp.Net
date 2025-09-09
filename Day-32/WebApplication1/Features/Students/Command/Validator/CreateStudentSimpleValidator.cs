using FluentValidation;
using WebApplication1.Features.Students.Command.Models;

namespace WebApplication1.Features.Students.Command.Validator;

public class CreateStudentSimpleValidator : AbstractValidator<CreateStudentSimpleDto>
{
    public CreateStudentSimpleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");
            
        RuleFor(x => x.Age)
            .GreaterThan(0).WithMessage("Age must be greater than 0")
            .LessThan(120).WithMessage("Age must be less than 120");
    }
}
