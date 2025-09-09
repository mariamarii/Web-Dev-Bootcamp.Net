using AutoMapper;
using MediatR;
using WebApplication1.Features.Students.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Student;
using WebApplication1.Dtos.StudentDto;
using System.Net;

namespace WebApplication1.Features.Students.Command.RemoveCourseFromStudent;

public class RemoveCourseFromStudentHandler(
    IGenericRepository<Student> studentRepository, 
    IGenericRepository<Course> courseRepository,
    IMapper mapper) : IRequestHandler<RemoveCourseFromStudentDto, Response>
{
    public async Task<Response> Handle(RemoveCourseFromStudentDto request, CancellationToken cancellationToken)
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

            // Check if student is enrolled in this course
            var courseToRemove = student.Courses.FirstOrDefault(c => c.Id == request.CourseId);
            if (courseToRemove == null)
            {
                return new Response(null, "Student is not enrolled in this course", HttpStatusCode.BadRequest);
            }

            // Remove course from student
            student.Courses.Remove(courseToRemove);
            await studentRepository.UpdateAsync(student, cancellationToken);

            var studentDto = mapper.Map<StudentSimpleDto>(student);
            return new Response(studentDto, "Course removed from student successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error removing course from student: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
