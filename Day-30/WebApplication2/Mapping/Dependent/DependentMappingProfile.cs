using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto.Dependent;

namespace WebApplication2.Mapping.Dependent;

public class DependentMappingProfile : Profile
{
    public DependentMappingProfile()
    {
        CreateMap<Models.Dependent, DependentReadDto>()
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : string.Empty));

        CreateMap<DependentWriteDto, Models.Dependent>();
    }
}
