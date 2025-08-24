using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class LoginController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddLogin([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var employeeExists = await context.Employees
            .AnyAsync(e => e.id == loginDto.employeeId, cancellationToken);

        if (!employeeExists)
        {
            return BadRequest($"Employee with ID {loginDto.employeeId} does not exist.");
        }
        var login = mapper.Map<Login>(loginDto);
        
        await context.Logins.AddAsync(login, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<LoginDto>(login);
        return CreatedAtAction(nameof(GetLoginById), new { id = login.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Login>>> GetLogins(CancellationToken cancellationToken)
    {
        var logins = await context.Logins
            .Include(l => l.employee) 
            .ToListAsync(cancellationToken);

        return mapper.Map<List<Login>>(logins);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Login>> GetLoginById(int id, CancellationToken cancellationToken)
    {
        var login = await context.Logins.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        if (login == null) return NotFound();

        return mapper.Map<Login>(login);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLogin(int id, [FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var login = await context.Logins.FindAsync(new object[] { id }, cancellationToken);
        if (login == null) return NotFound();

        mapper.Map(loginDto, login);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLogin(int id, CancellationToken cancellationToken)
    {
        var login = await context.Logins.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        if (login == null) return NotFound();

        context.Logins.Remove(login);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
