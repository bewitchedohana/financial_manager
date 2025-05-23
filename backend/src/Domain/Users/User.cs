using Domain.Shared;
using Domain.Tests.SharedTests;

namespace Domain.Users;

public class User : BaseEntity
{
    public Email Email { get; private set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public Username Username { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with the specified email, first name, last name, and username.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="username">The username of the user.</param>
    private User(
        Email email,
        string firstName,
        string lastName,
        Username username) : base()
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="username">The username of the user.</param>
    /// <returns>The new instance of the <see cref="User"/> class.</returns>
    public static User Create(
        Email email,
        string firstName,
        string lastName,
        Username username)
    {
        return new User(email, firstName, lastName, username);
    }
}