using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Features.Courses.Command.Models;
using WebApplication1.Features.Courses.Query.Models;
using WebApplication1.Dtos.CourseDto;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllCourses([FromQuery] CourseFilterDto filter)
    {
        var query = new GetAllCoursesQuery
        {
            Code = filter.Code,
            Title = filter.Title,
            MinHours = filter.MinHours,
            MaxHours = filter.MaxHours,
            StudentName = filter.StudentName,
            MinStudentAge = filter.MinStudentAge,
            MaxStudentAge = filter.MaxStudentAge,
            HasStudents = filter.HasStudents,
            SortBy = filter.SortBy,
            IsDescending = filter.IsDescending,
            Page = filter.Page,
            PageSize = filter.PageSize
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
