using MediatR;
using Opas.Domain.Abstractions;

namespace Opas.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}