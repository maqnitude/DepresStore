namespace DepresStore.Shared.Kernel.Application
{
    /// <summary>
    /// Represents a request with a response.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public interface IRequest<out TResponse> { }

    /// <summary>
    /// Represents a request with a no response.
    /// </summary>
    public interface IRequest { }
}