using AutoMapper;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Mapping;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<LoginDto, Login>().ReverseMap();

    }
}