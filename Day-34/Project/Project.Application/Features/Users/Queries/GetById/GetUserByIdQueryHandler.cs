using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Users.Dtos;
using Project.Application.Features.Users.Specifications;
using Project.Domain.Models.Users;
using Project.Domain.Responses;

namespace Project.Application.Features.Users.Queries.GetById;

public class GetUserByIdQueryHandler(IMapper mapper, IRepository<User> userRepository) 
    : IQueryHandler<GetUserByIdQuery, UserDto>
{
    public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new UserByIdSpec(request.Id);
        var user = await userRepository.FirstOrDefaultAsync(spec, cancellationToken);
        
        if (user == null)
            return Response<UserDto>.Failure("User not found");

        var userDto = mapper.Map<UserDto>(user);
        return Response<UserDto>.Success(userDto);
    }
}