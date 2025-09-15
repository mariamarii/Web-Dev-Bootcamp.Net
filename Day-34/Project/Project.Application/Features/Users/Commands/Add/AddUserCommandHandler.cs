using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Users;
using Project.Domain.Responses;

namespace Project.Application.Features.Users.Commands.Add;

public class AddUserCommandHandler(IMapper mapper, IRepository<User> userRepository) 
    : ICommandHandler<AddUserCommand, string>
{
    public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        await userRepository.AddAsync(user, cancellationToken);
        return Response<string>.Success();
    }
}