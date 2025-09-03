using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto.Department;
using DepartmentModel = Department;

namespace WebApplication2.Mapping.Department;

public class DepartmentMappingProfile : Profile
{
    public DepartmentMappingProfile()
    {
        CreateMap<DepartmentModel, DepartmentReadDto>()
            .ForMember(d => d.Employees, opt => opt.MapFrom(src => src.Employees.Select(e => e.Name)))
            .ForMember(d => d.Managers, opt => opt.MapFrom(src => src.Managers.Select(m => new ManagerDto { 
                EmployeeId = m.EmployeeId, 
                EmployeeName = m.Employee != null ? m.Employee.Name : string.Empty, 
                Since = m.Since 
            })))
            .ForMember(d => d.Projects, opt => opt.MapFrom(src => src.Projects.Select(p => p.Name)));

        CreateMap<DepartmentWriteDto, DepartmentModel>();
    }
}
