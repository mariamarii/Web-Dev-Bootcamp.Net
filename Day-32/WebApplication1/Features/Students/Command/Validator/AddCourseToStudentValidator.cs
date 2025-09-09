using FluentValidation;
using WebApplication1.Features.Students.Command.Models;

namespace WebApplication1.Features.Students.Command.Validator;

public class AddCourseToStudentValidator : AbstractValidator<AddCourseToStudentDto>
{
    public AddCourseToStudentValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");
            
        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("Course ID must be greater than 0");
    }
}
