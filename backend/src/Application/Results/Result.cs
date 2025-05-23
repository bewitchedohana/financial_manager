namespace Application.Results;

internal sealed class Result<T>(ErrorResult? error, T? result = null) where T : class
{
    public ErrorResult? Error { get; private set; } = error;
    public T? Value { get; private set; } = result;
}

internal static class Result
{
    public static Result<TResult> Success<TResult>(TResult result)
        where TResult : class
        => new(null, result);

    public static Result<TResult> Failure<TResult>(ErrorResult error)
        where TResult : class
        => new(error, null);
}