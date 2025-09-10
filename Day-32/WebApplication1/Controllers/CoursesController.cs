using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Features.Courses.Command.Models;
using WebApplication1.Features.Courses.Query.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllCourses(
        [FromQuery] string? code = null,
        [FromQuery] string? title = null,
        [FromQuery] int? minHours = null,
        [FromQuery] int? maxHours = null,
        [FromQuery] string? studentName = null,
        [FromQuery] int? minStudentAge = null,
        [FromQuery] int? maxStudentAge = null,
        [FromQuery] bool? hasStudents = null,
        [FromQuery] string? sortBy = "Title",
        [FromQuery] bool isDescending = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetAllCoursesQuery
        {
            Code = code,
            Title = title,
            MinHours = minHours,
            MaxHours = maxHours,
            StudentName = studentName,
            MinStudentAge = minStudentAge,
            MaxStudentAge = maxStudentAge,
            HasStudents = hasStudents,
            SortBy = sortBy,
            IsDescending = isDescending,
            Page = page,
            PageSize = pageSize
        };
        
        var response = await mediator.Send(query);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var query = new GetCourseByIdQuery(id);
        var response = await mediator.Send(query);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator.Send(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto dto)
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
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var dto = new DeleteCourseDto { Id = id };
        var response = await mediator.Send(dto);
        return StatusCode((int)response.StatusCode, response);
    }
}
