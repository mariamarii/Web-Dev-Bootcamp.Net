using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Users.Dtos;
using Project.Application.Features.Users.Specifications;
using Project.Domain.Models.Users;
using Project.Domain.Responses;

namespace Project.Application.Features.Users.Queries.GetAll;

public class GetAllUsersQueryHandler(IMapper mapper, IRepository<User> userRepository)
    : IQueryHandler<GetAllUsersQuery, List<UserDto>>
{
    public async Task<Response<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var spec = new UsersSpec();
        var users = await userRepository.ListAsync(spec, cancellationToken);
        var userDtos = mapper.Map<List<UserDto>>(users);
        return Response<List<UserDto>>.Success(userDtos);
    }
}