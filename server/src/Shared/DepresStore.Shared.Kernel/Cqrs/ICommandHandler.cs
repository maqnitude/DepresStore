using DepresStore.Shared.Kernel.Mediator;

namespace DepresStore.Shared.Kernel.Cqrs
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