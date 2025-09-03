using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dto;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Data;
using WebApplication2.Dto.Project;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(
    IGenericRepository<Project> repo,
    IMapper mapper,
    ApplicationDbContext context
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var items = await repo.GetAllAsync(pageNumber, pageSize);
        return Ok(mapper.Map<IEnumerable<ProjectReadDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await repo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(mapper.Map<ProjectReadDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectWriteDto dto)
    {
        var project = mapper.Map<Project>(dto);
        repo.Add(project);
        await repo.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProjectWriteDto dto)
    {
        var project = await repo.GetByIdAsync(id);
        if (project == null) return NotFound();

        mapper.Map(dto, project);
        repo.Update(project);
        await repo.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await repo.GetByIdAsync(id);
        if (project == null) return NotFound();

        repo.Delete(project);
        await repo.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{projectId}/assign/{employeeId}")]
    public async Task<IActionResult> AssignEmployee(int projectId, int employeeId, [FromQuery] int hours = 0)
    {
        var ep = await context.EmployeeProjects.FindAsync(employeeId, projectId);
        if (ep != null) return BadRequest("Already assigned");

        var newEP = new EmployeeProject 
        { 
            EmployeeId = employeeId, 
            ProjectId = projectId, 
            Hours = hours 
        };

        context.EmployeeProjects.Add(newEP);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{projectId}/assign/{employeeId}")]
    public async Task<IActionResult> RemoveEmployeeFromProject(int projectId, int employeeId)
    {
        var ep = await context.EmployeeProjects.FindAsync(employeeId, projectId);
        if (ep == null) return NotFound();

        context.EmployeeProjects.Remove(ep);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
