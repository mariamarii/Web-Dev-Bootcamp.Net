using AutoMapper;
using WebApplication2.Dto;
using WebApplication2.Models;
namespace WebApplication2.Mapping;

public class EmployeeImageUrlResolver(IHttpContextAccessor http) 
    : IValueResolver<Employee, EmployeeReadDto, string?>
{
    public string? Resolve(Employee source, EmployeeReadDto dest, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ImagePath)) return null;
        
        var req = http.HttpContext?.Request;
        return $"{req?.Scheme}://{req?.Host}{source.ImagePath}";
    }
}