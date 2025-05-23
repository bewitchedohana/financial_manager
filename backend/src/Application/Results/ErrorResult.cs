namespace Application.Results;

public class ErrorResult(string code, string message) : IEquatable<ErrorResult>
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;

    public static ErrorResult None => new(string.Empty, string.Empty);

    public bool Equals(ErrorResult? other)
        => other is not null
            && Code == other.Code
            && Message == other.Message;
}