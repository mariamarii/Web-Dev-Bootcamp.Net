using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Users;
using Project.Domain.Responses;

namespace Project.Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler(IRepository<User> userRepository)
    : ICommandHandler<DeleteUserCommand, string>
{
    public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            return Response<string>.Failure("User not found");

        await userRepository.DeleteAsync(user, cancellationToken);
        return Response<string>.Success("User deleted successfully");
    }
}