using FluentValidation;
using WebApplication1.Features.Students.Command.Models;

namespace WebApplication1.Features.Students.Command.Validator;

public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentValidator()
    {
        RuleFor( x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age is required");
    }
}