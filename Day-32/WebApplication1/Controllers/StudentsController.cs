using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Features.Students.Command.Models;
using WebApplication1.Features.Students.Query.Models;
using WebApplication1.Features.Students.Command.RemoveCourseFromStudent;
using WebApplication1.Dtos.StudentDto;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllStudents([FromQuery] StudentFilterDto filter)
    {
        var query = new GetAllStudentsQuery
        {
            Name = filter.Name,
            MinAge = filter.MinAge,
            MaxAge = filter.MaxAge,
            CourseCode = filter.CourseCode,
            CourseTitle = filter.CourseTitle,
            HasCourses = filter.HasCourses,
            SortBy = filter.SortBy,
            IsDescending = filter.IsDescending,
            Page = filter.Page,
            PageSize = filter.PageSize
        };

        var response = await mediator.Send(query);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var query = new GetStudentByIdQuery(id);
        var response = await mediator.Send(query);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentSimpleDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator.Send(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("{studentId}/courses/{courseId}")]
    public async Task<IActionResult> AddCourseToStudent(int studentId, int courseId)
    {
        var dto = new AddCourseToStudentDto
        {
            StudentId = studentId,
            CourseId = courseId
        };

        var response = await mediator.Send(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{studentId}/courses/{courseId}")]
    public async Task<IActionResult> RemoveCourseFromStudent(int studentId, int courseId)
    {
        var dto = new RemoveCourseFromStudentDto
        {
            StudentId = studentId,
            CourseId = courseId
        };

        var response = await mediator.Send(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
    {
        dto.Id = id; // Set the ID from the route parameter

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator.Send(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var dto = new DeleteStudentDto { Id = id };
        var response = await mediator.Send(dto);
        return StatusCode((int)response.StatusCode, response);
    }
}
