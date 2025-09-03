using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dto;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Data;
using WebApplication2.Dto.Department;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController(
    IGenericRepository<Department> repo,
    IMapper mapper,
    ApplicationDbContext context
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var items = await repo.GetAllAsync(pageNumber, pageSize);
        return Ok(mapper.Map<IEnumerable<DepartmentReadDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await repo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(mapper.Map<DepartmentReadDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentWriteDto dto)
    {
        var dept = mapper.Map<Department>(dto);
        repo.Add(dept);
        await repo.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DepartmentWriteDto dto)
    {
        var dept = await repo.GetByIdAsync(id);
        if (dept == null) return NotFound();

        mapper.Map(dto, dept);
        repo.Update(dept);
        await repo.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dept = await repo.GetByIdAsync(id);
        if (dept == null) return NotFound();

        repo.Delete(dept);
        await repo.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{departmentId}/manager/{employeeId}")]
    public async Task<IActionResult> AddManager(int departmentId, int employeeId, [FromQuery] DateTime? since)
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

    [HttpDelete("{departmentId}/manager/{employeeId}")]
    public async Task<IActionResult> RemoveManagerFromDepartment(int departmentId, int employeeId)
    {
        var dm = await context.DepartmentManagers.FindAsync(employeeId, departmentId);
        if (dm == null) return NotFound();

        context.DepartmentManagers.Remove(dm);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
