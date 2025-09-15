using AutoMapper;
using Project.Application.Features.Users.Commands.Add;
using Project.Application.Features.Users.Commands.Update;
using Project.Application.Features.Users.Dtos;
using Project.Domain.Models.Users;

namespace Project.Application.Mapping.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserCommand, Domain.Models.Users.User>();
        CreateMap<Domain.Models.Users.User, UserDto>();
        CreateMap<Domain.Models.Users.User, UpdateUserCommand>().ReverseMap();
    }
}