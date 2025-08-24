using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] EmployeeWriteDto employeeDto, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(employeeDto);

        await context.Employees.AddAsync(employee, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var employeeReadDto = mapper.Map<EmployeeReadDto>(employee);

        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.id }, employeeReadDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetEmployees(CancellationToken cancellationToken)
    {
        var employees = await context.Employees
            .Include(e => e.department)
            .Include(e => e.role)
            .Include(e => e.login)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<EmployeeReadDto>>(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeReadDto>> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        var employee = await context.Employees
            .Include(e => e.department)
            .Include(e => e.role)
            .Include(e => e.login)
            .FirstOrDefaultAsync(e => e.id == id, cancellationToken);

        if (employee == null) return NotFound();

        return mapper.Map<EmployeeReadDto>(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeWriteDto employeeDto, CancellationToken cancellationToken)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e =>e.id == id , cancellationToken);
        if (employee == null) return NotFound();

        mapper.Map(employeeDto, employee);

        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e =>e.id == id , cancellationToken);
        if (employee == null) return NotFound();

        context.Employees.Remove(employee);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
