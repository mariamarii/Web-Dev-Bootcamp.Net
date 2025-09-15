using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Users.Dtos;

namespace Project.Application.Features.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery<UserDto>;