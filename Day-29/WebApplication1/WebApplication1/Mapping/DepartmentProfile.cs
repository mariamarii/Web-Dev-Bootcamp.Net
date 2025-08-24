using AutoMapper;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentDto, Department>().ReverseMap();

    }
}