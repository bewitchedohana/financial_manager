namespace Application.Results;

public sealed class Result<T>
{
    public bool IsSuccess => Value is not null || Errors.Count == 0;
    public T? Value { get; private set; }
    public List<string> Errors { get; private set; } = [];

    private Result(T success)
    {
        Value = success;
    }

    private Result(string error)
    {
        Errors.Add(error);
    }

    private Result(IEnumerable<string> errors)
    {
        Errors.AddRange(errors);
    }

    public static Result<T> Create(IEnumerable<string> errors)
        => new(errors);

    public static Result<T> Create(string error)
        => new(error);

    public static Result<T> Create(T value)
        => new(value);
}

public static class Result
{
    public static Result<TResult> Success<TResult>(TResult value) => Result<TResult>.Create(value);

    public static Result<TResult> Failure<TResult>(string error) => Result<TResult>.Create(error);

    public static Result<TResult> Failure<TResult>(IEnumerable<string> errors) => Result<TResult>.Create(errors);
}