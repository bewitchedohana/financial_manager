using Application.Results;

namespace Application.Abstractions;

internal abstract class UseCase<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    public abstract Result<TResponse> Execute(TRequest request);
}