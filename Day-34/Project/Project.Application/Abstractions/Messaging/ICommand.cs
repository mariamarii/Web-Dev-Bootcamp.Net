using MediatR;
using Project.Domain.Responses;

namespace Project.Application.Abstractions.Messaging;

// public interface ICommand<TRequest> : IRequest<Response>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Response<TResponse>>;
