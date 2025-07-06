using DepresStore.Shared.Kernel.Mediator;

namespace DepresStore.Shared.Kernel.Cqrs
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }
}