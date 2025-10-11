public sealed class DomainException : Exception
{
    public DomainException()
    { }

    public DomainException(string message) : base(message)
    { }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    { }

    /// <summary>
    /// Throws a <see cref="DomainException"/> if the given condition is true.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="message">The exception message to use if the condition is true.</param>
    public static void When(bool condition, string message)
    {
        if (condition)
        {
            throw new DomainException(message);
        }
    }

    /// <summary>
    /// Throws a <see cref="DomainException"/> if the specified property is null or empty.
    /// </summary>
    /// <param name="property">The string property to check.</param>
    /// <param name="message">The exception message to use if the property is null or empty.</param>
    public static void ThrowWhenNullOrEmpty(string property, string message)
    {
        When(string.IsNullOrEmpty(property), message);
    }

    /// <summary>
    /// Throws an exception when the given property is null.
    /// </summary>
    /// <param name="property">The value to check for null.</param>
    /// <param name="message">The exception message.</param>
    public static void ThrowWhenNull(object property, string message)
    {
        When(property is null, message);
    }
}