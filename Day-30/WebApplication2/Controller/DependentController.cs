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
public class DependentController(
    IGenericRepository<Dependent> repo,
    IMapper mapper,
    ApplicationDbContext context
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var items = await context.Dependents
            .Include(d => d.Employee)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return Ok(mapper.Map<IEnumerable<DependentReadDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await context.Dependents
            .Include(d => d.Employee)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        if (item == null) return NotFound();
        return Ok(mapper.Map<DependentReadDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create(DependentWriteDto dto)
    {
        var dep = mapper.Map<Dependent>(dto);
        repo.Add(dep);
        await context.SaveChangesAsync();

        
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DependentWriteDto dto)
    {
        var dep = await context.Dependents.FindAsync(id);
        if (dep == null) return NotFound();

        mapper.Map(dto, dep);
        repo.Update(dep);
        await context.SaveChangesAsync();

        

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dep = await context.Dependents.FindAsync(id);
        if (dep == null) return NotFound();

        repo.Delete(dep);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
