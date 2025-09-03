using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto.Employee;
using EmployeeModel = WebApplication2.Models.Employee;

namespace WebApplication2.Mapping.Employee;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<EmployeeModel, EmployeeReadDto>()
            .ForMember(d => d.ImageUrl, opt => opt.MapFrom<EmployeeImageUrlResolver>())
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(d => d.ProjectNames, opt => opt.MapFrom(src => src.EmployeeProjects.Select(ep => ep.Project != null ? ep.Project.Name : string.Empty)))
            .ForMember(d => d.ManagedDepartments, opt => opt.MapFrom(src => src.ManagedDepartments.Select(md => new ManagedDepartmentDto {
                DepartmentId = md.DepartmentId,
                DepartmentName = md.Department != null ? md.Department.Name : string.Empty,
                Since = md.Since
            })));

        CreateMap<EmployeeWriteDto, EmployeeModel>()
            .ForMember(dest => dest.EmployeeProjects, opt => opt.Ignore())
            .ForMember(dest => dest.ManagedDepartments, opt => opt.Ignore());
    }
}
