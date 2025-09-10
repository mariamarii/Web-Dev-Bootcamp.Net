using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Features.Students.Command.Models;
using WebApplication1.Features.Students.Query.Models;
using WebApplication1.Features.Students.Command.RemoveCourseFromStudent;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllStudents(
        [FromQuery] string? name = null,
        [FromQuery] int? minAge = null,
        [FromQuery] int? maxAge = null,
        [FromQuery] string? courseCode = null,
        [FromQuery] string? courseTitle = null,
        [FromQuery] bool? hasCourses = null,
        [FromQuery] string? sortBy = "Name",
        [FromQuery] bool isDescending = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetAllStudentsQuery
        {
            Name = name,
            MinAge = minAge,
            MaxAge = maxAge,
            CourseCode = courseCode,
            CourseTitle = courseTitle,
            HasCourses = hasCourses,
            SortBy = sortBy,
            IsDescending = isDescending,
            Page = page,
            PageSize = pageSize
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
