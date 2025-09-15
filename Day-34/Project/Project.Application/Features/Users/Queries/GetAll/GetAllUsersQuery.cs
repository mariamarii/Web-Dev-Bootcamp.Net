using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Users.Dtos;
using Project.Domain.Filters;

namespace Project.Application.Features.Users.Queries.GetAll;

public record GetAllUsersQuery(BaseFilter Filter) : IQuery<List<UserDto>>;