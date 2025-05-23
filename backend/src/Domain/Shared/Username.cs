using System.Text.RegularExpressions;

namespace Domain.Shared;

public sealed partial class Username
{
    private string _value = string.Empty;

    public string Value
    {
        get => _value;
        private set
        {
            DomainException.ThrowWhenNullOrEmpty(value, "Username cannot be empty.");
            DomainException.When(value.Length > 16, "Username cannot be longer than 16 characters.");
            DomainException.When(value.Length < 5, "Username cannot be shorter than 5 characters.");
            DomainException.When(SpaceRegex().IsMatch(value), "Username cannot contain spaces.");
            DomainException.When(UpperCaseRegex().IsMatch(value), "Username cannot contain uppercase letters.");
            _value = value;
        }
    }

    public static implicit operator Username(string username) => new(username);

    public Username(string username)
    {
        Value = username;
    }

    [GeneratedRegex(@"\s")]
    private static partial Regex SpaceRegex();
    [GeneratedRegex(@"[A-Z]")]
    private static partial Regex UpperCaseRegex();
}