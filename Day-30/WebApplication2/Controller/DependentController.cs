using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dto;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Dto.Dependent;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DependentController(
    IGenericRepository<Dependent> repo,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var items = await repo.GetAllAsync(pageNumber, pageSize);
        return Ok(mapper.Map<IEnumerable<DependentReadDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await repo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(mapper.Map<DependentReadDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create(DependentWriteDto dto)
    {
        var dep = mapper.Map<Dependent>(dto);
        repo.Add(dep);
        await repo.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DependentWriteDto dto)
    {
        var dep = await repo.GetByIdAsync(id);
        if (dep == null) return NotFound();

        mapper.Map(dto, dep);
        repo.Update(dep);
        await repo.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dep = await repo.GetByIdAsync(id);
        if (dep == null) return NotFound();

        repo.Delete(dep);
        await repo.SaveChangesAsync();
        return NoContent();
    }
}
