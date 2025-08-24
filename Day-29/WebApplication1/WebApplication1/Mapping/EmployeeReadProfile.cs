using AutoMapper;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Mapping;

public class EmployeeReadProfile : Profile
{
    public EmployeeReadProfile()
    {
        CreateMap<Employee, EmployeeReadDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.department.Name))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.role.Name))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.login.username));
    }
}