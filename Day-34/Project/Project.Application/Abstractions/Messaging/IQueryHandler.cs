using MediatR;
using Project.Domain.Responses;

namespace Project.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Response<TResponse>>
    where TQuery : IQuery<TResponse>;
