using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dto;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

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
        var items = await context.Projects
            .Include(p => p.Department)
            .Include(p => p.EmployeeProjects).ThenInclude(ep => ep.Employee)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return Ok(mapper.Map<IEnumerable<ProjectReadDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await context.Projects
            .Include(p => p.Department)
            .Include(p => p.EmployeeProjects).ThenInclude(ep => ep.Employee)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (item == null) return NotFound();
        return Ok(mapper.Map<ProjectReadDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectWriteDto dto)
    {
        var project = mapper.Map<Project>(dto);
        repo.Add(project);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProjectWriteDto dto)
    {
        var project = await context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        mapper.Map(dto, project);
        repo.Update(project);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        repo.Delete(project);
        await context.SaveChangesAsync();
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
