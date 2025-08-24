using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class RoleController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto, CancellationToken cancellationToken)
    {
        var role = mapper.Map<Role>(roleDto);
        await context.Roles.AddAsync(role, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<RoleDto>(role);
        return CreatedAtAction(nameof(GetRoleById), new { id = role.id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles(CancellationToken cancellationToken)
    {
        var roles = await context.Roles.Include(r => r.Employees).ToListAsync(cancellationToken);
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRoleById(int id, CancellationToken cancellationToken)
    {
        var role = await context.Roles.Include(r => r.Employees).FirstOrDefaultAsync(r => r.id == id, cancellationToken);
        if (role == null) return NotFound();

        return Ok(role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleDto, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FirstOrDefaultAsync(r => r.id == id, cancellationToken);
        if (role == null) return NotFound();

        mapper.Map(roleDto, role);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FirstOrDefaultAsync(r => r.id == id, cancellationToken);
        if (role == null) return NotFound();

        context.Roles.Remove(role);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
