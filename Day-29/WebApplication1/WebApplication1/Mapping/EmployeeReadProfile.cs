using AutoMapper;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Mapping;

public class EmployeeReadProfile : Profile
{
    public EmployeeReadProfile()
    {
        CreateMap<Employee, EmployeeReadDto>();
            
    }
}