using FluentValidation;
using WebApplication1.Features.Students.Command.Models;

namespace WebApplication1.Features.Students.Command.RemoveCourseFromStudent;

public class RemoveCourseFromStudentValidator : AbstractValidator<RemoveCourseFromStudentDto>
{
    public RemoveCourseFromStudentValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");
            
        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("Course ID must be greater than 0");
    }
}
