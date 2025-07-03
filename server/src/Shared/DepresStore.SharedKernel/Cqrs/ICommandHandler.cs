using DepresStore.SharedKernel.Mediator;

namespace DepresStore.SharedKernel.Cqrs
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }

    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest>
        where TRequest : IRequest
    {
    }
}