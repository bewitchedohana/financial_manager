using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class User : IdentityUser
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with the specified email, first name, last name, and username.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    public User(
        Email email,
        string firstName,
        string lastName,
        string passwordHash) : base()
    {
        Email = email.Value;
        UserName = email.Value;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
    }

    public User() { }

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
        string passwordHash)
    {
        return new User(email, firstName, lastName, passwordHash);
    }
}