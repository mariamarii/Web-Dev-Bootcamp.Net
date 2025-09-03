using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto.Project;

namespace WebApplication2.Mapping.Project;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Models.Project, ProjectReadDto>()
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(d => d.Employees, opt => opt.MapFrom(src => src.EmployeeProjects.Select(ep => new EmployeeOnProjectDto { 
                EmployeeId = ep.EmployeeId, 
                EmployeeName = ep.Employee != null ? ep.Employee.Name : string.Empty, 
                Hours = ep.Hours 
            })));

        CreateMap<ProjectWriteDto, Models.Project>();
    }
}
