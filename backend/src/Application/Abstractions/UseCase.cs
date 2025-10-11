using Application.Results;

namespace Application.Abstractions;

public abstract class UseCase<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    public abstract Task<Result<TResponse>> Execute(TRequest request);
}