using AutoMapper;
using WebApplication2.Dto.Employee;
using WebApplication2.Models;
using EmployeeModel = WebApplication2.Models.Employee;

namespace WebApplication2.Mapping.Employee;

public class EmployeeImageUrlResolver(IHttpContextAccessor http) 
    : IValueResolver<EmployeeModel, EmployeeReadDto, string?>
{
    public string? Resolve(EmployeeModel source, EmployeeReadDto dest, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ImagePath)) return null;
        
        var req = http.HttpContext?.Request;
        return $"{req?.Scheme}://{req?.Host}{source.ImagePath}";
    }
}
