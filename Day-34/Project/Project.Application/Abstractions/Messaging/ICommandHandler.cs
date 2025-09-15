using System.Windows.Input;
using MediatR;
using Project.Domain.Responses;

namespace Project.Application.Abstractions.Messaging;

// public interface ICommandHandler<in TCommand>
//     : IRequestHandler<TCommand, Response<>>
//     where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Response<TResponse>>
    where TCommand : ICommand<TResponse>;
