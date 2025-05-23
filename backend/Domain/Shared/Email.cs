using System.Net.Mail;

namespace Domain.Shared;

public class Email
{
    private const string errorMessage = "Invalid email address.";
    private string _value = string.Empty;

    public string Value
    {
        get => _value;
        private set
        {
            string trimmed = value.Trim();
            DomainException.When(trimmed.EndsWith("."), errorMessage);
            try
            {
                _ = new MailAddress(trimmed);
            }
            catch
            {
                throw new DomainException(errorMessage);
            }
        }
    }
    
    public static implicit operator Email(string email) => new(email);

    public Email(string email)
    {
        Value = email;
    }
}