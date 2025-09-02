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
public class EmployeeController(
    IGenericRepository<Employee> repo,
    IFileUpload fileUpload,
    IMapper mapper,
    ApplicationDbContext context
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var items = await context.Employees
            .Include(e => e.Department)
            .Include(e => e.EmployeeProjects).ThenInclude(ep => ep.Project)
            .Include(e => e.ManagedDepartments).ThenInclude(md => md.Department)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return Ok(mapper.Map<IEnumerable<EmployeeReadDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var e = await context.Employees
            .Include(x => x.Department)
            .Include(x => x.EmployeeProjects).ThenInclude(ep => ep.Project)
            .Include(x => x.ManagedDepartments).ThenInclude(md => md.Department)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (e == null) return NotFound();
        return Ok(mapper.Map<EmployeeReadDto>(e));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] EmployeeWriteDto dto)
    {
        var entity = mapper.Map<Employee>(dto);

        if (dto.Image != null) 
            entity.ImagePath = await fileUpload.UploadEmployeeImageAsync(dto.Image);

        entity.EmployeeProjects = dto.ProjectIds
            .Select(pid => new EmployeeProject { ProjectId = pid, Employee = entity, Hours = 0 })
            .ToList();

        repo.Add(entity);
        await context.SaveChangesAsync();

        
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] EmployeeWriteDto dto)
    {
        var entity = await context.Employees
            .Include(e => e.EmployeeProjects)
            .Include(e => e.ManagedDepartments)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null) return NotFound();

        mapper.Map(dto, entity);

        if (dto.Image != null) 
            entity.ImagePath = await fileUpload.UploadEmployeeImageAsync(dto.Image);

        entity.EmployeeProjects.Clear();
        var newEPS = dto.ProjectIds
            .Select(pid => new EmployeeProject { EmployeeId = id, ProjectId = pid, Hours = 0 })
            .ToList();
        foreach (var ep in newEPS) entity.EmployeeProjects.Add(ep);

        repo.Update(entity);
        await context.SaveChangesAsync();

        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await context.Employees.FindAsync(id);
        if (entity == null) return NotFound();

        repo.Delete(entity);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{employeeId}/manage/{departmentId}")]
    public async Task<IActionResult> AssignManager(int employeeId, int departmentId, [FromQuery] DateTime? since)
    {
        var emp = await context.Employees.FindAsync(employeeId);
        var dept = await context.Departments.FindAsync(departmentId);
        if (emp == null || dept == null) return NotFound();

        var existing = await context.DepartmentManagers.FindAsync(employeeId, departmentId);
        if (existing != null) return BadRequest("Already manager");

        var dm = new DepartmentManager 
        { 
            EmployeeId = employeeId, 
            DepartmentId = departmentId, 
            Since = since ?? DateTime.UtcNow 
        };
        context.DepartmentManagers.Add(dm);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{employeeId}/manage/{departmentId}")]
    public async Task<IActionResult> RemoveManager(int employeeId, int departmentId)
    {
        var dm = await context.DepartmentManagers.FindAsync(employeeId, departmentId);
        if (dm == null) return NotFound();
        context.DepartmentManagers.Remove(dm);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{employeeId}/project/{projectId}/hours")]
    public async Task<IActionResult> SetHours(int employeeId, int projectId, [FromQuery] int hours)
    {
        var ep = await context.EmployeeProjects.FindAsync(employeeId, projectId);
        if (ep == null) return NotFound();
        ep.Hours = hours;
        context.EmployeeProjects.Update(ep);
        await context.SaveChangesAsync();
        return Ok();
    }
}
