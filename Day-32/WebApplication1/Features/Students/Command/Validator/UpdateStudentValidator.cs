using FluentValidation;
using WebApplication1.Features.Students.Command.Models;

namespace WebApplication1.Features.Students.Command.Validator;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentValidator()
    {
       
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age is required");
    }
}
