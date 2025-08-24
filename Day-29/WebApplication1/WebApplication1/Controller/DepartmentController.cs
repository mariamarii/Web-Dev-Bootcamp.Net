using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var department = mapper.Map<Department>(departmentDto);
        await context.Departments.AddAsync(department, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<DepartmentDto>(department);
        return CreatedAtAction(nameof(GetDepartmentById), new { id = department.id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments(CancellationToken cancellationToken)
    {
        var departments = await context.Departments.ToListAsync(cancellationToken);
        return mapper.Map<List<Department>>(departments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartmentById(int id, CancellationToken cancellationToken)
    {
        var department = await context.Departments.FirstOrDefaultAsync(d => d.id == id, cancellationToken);
        if (department == null) return NotFound();

        return mapper.Map<Department>(department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var department = await context.Departments.FirstOrDefaultAsync(d => d.id == id, cancellationToken);
        if (department == null) return NotFound();

        mapper.Map(departmentDto, department);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
    {
        var department = await context.Departments.FirstOrDefaultAsync(d => d.id == id, cancellationToken);
        if (department == null) return NotFound();

        context.Departments.Remove(department);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
