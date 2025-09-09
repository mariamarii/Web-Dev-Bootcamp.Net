using FluentValidation;
using WebApplication1.Features.Students.Command.Models;

namespace WebApplication1.Features.Students.Command.Validator;

public class DeleteStudentValidator : AbstractValidator<DeleteStudentDto>
{
    public DeleteStudentValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
    }
}
