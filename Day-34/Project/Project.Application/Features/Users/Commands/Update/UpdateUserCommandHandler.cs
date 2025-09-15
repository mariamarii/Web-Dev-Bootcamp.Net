using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Users;
using Project.Domain.Responses;

namespace Project.Application.Features.Users.Commands.Update;

public class UpdateUserCommandHandler(IMapper mapper, IRepository<User> userRepository) 
    : ICommandHandler<UpdateUserCommand, Guid>
{
    public async Task<Response<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            return Response<Guid>.Failure("User not found");
        
        mapper.Map(request, user);
        await userRepository.UpdateAsync(user, cancellationToken);
        return Response<Guid>.Success(user.Id);
    }
}