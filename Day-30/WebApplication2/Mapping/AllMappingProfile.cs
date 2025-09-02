// Mapping/MappingProfile.cs
using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Mapping;
public class ALLMappingProfile : Profile
{
    public ALLMappingProfile()
    {
        CreateMap<Employee, EmployeeReadDto>()
            .ForMember(d => d.ImageUrl, opt => opt.MapFrom<EmployeeImageUrlResolver>())
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(d => d.ProjectNames, opt => opt.MapFrom(src => src.EmployeeProjects.Select(ep => ep.Project != null ? ep.Project.Name : string.Empty)))
            .ForMember(d => d.ManagedDepartments, opt => opt.MapFrom(src => src.ManagedDepartments.Select(md => new ManagedDepartmentDto {
                DepartmentId = md.DepartmentId,
                DepartmentName = md.Department != null ? md.Department.Name : string.Empty,
                Since = md.Since
            })));

        CreateMap<EmployeeWriteDto, Employee>()
            .ForMember(dest => dest.EmployeeProjects, opt => opt.Ignore())
            .ForMember(dest => dest.ManagedDepartments, opt => opt.Ignore());

        CreateMap<Department, DepartmentReadDto>()
            .ForMember(d => d.Employees, opt => opt.MapFrom(src => src.Employees.Select(e => e.Name)))
            .ForMember(d => d.Managers, opt => opt.MapFrom(src => src.Managers.Select(m => new ManagerDto { EmployeeId = m.EmployeeId, EmployeeName = m.Employee != null ? m.Employee.Name : string.Empty, Since = m.Since })))
            .ForMember(d => d.Projects, opt => opt.MapFrom(src => src.Projects.Select(p => p.Name)));
        CreateMap<DepartmentWriteDto, Department>();

        CreateMap<Project, ProjectReadDto>()
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(d => d.Employees, opt => opt.MapFrom(src => src.EmployeeProjects.Select(ep => new EmployeeOnProjectDto { EmployeeId = ep.EmployeeId, EmployeeName = ep.Employee != null ? ep.Employee.Name : string.Empty, Hours = ep.Hours })));
        CreateMap<ProjectWriteDto, Project>();

        CreateMap<Dependent, DependentReadDto>()
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty));
        CreateMap<DependentWriteDto, Dependent>();
    }
}
