using MediatR;
using Project.Domain.Responses;

namespace Project.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Response<TResponse>>;
