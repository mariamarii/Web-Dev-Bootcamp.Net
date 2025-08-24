using AutoMapper;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Mapping;

public class EmployeeWriteProfile : Profile
{
    public EmployeeWriteProfile()
    {
        CreateMap<Employee, EmployeeWriteDto>().ReverseMap();

    }
}