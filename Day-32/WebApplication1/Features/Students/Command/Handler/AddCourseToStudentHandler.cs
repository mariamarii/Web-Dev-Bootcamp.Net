using AutoMapper;
using MediatR;
using WebApplication1.Features.Students.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Student;
using WebApplication1.Specifications.Course;
using WebApplication1.Dtos.StudentDto;
using System.Net;

namespace WebApplication1.Features.Students.Command.Handler;

public class AddCourseToStudentHandler(
    IGenericRepository<Student> studentRepository, 
    IGenericRepository<Course> courseRepository,
    IMapper mapper) : IRequestHandler<AddCourseToStudentDto, Response>
{
    public async Task<Response> Handle(AddCourseToStudentDto request, CancellationToken cancellationToken)
    {
        try
        {
            // Get student with courses
            var studentSpec = new StudentByIdWithCoursesSpec(request.StudentId);
            var student = await studentRepository.FirstOrDefaultAsync(studentSpec, cancellationToken);
            
            if (student == null)
            {
                return new Response(null, "Student not found", HttpStatusCode.NotFound);
            }

            // Get course
            var course = await courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            
            if (course == null)
            {
                return new Response(null, "Course not found", HttpStatusCode.NotFound);
            }

            // Check if student already enrolled in this course
            if (student.Courses.Any(c => c.Id == request.CourseId))
            {
                return new Response(null, "Student is already enrolled in this course", HttpStatusCode.BadRequest);
            }

            // Add course to student
            student.Courses.Add(course);
            await studentRepository.UpdateAsync(student, cancellationToken);

            var studentDto = mapper.Map<StudentSimpleDto>(student);
            return new Response(studentDto, "Course added to student successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error adding course to student: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
