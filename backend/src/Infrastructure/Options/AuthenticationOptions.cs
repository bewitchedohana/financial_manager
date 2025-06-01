namespace Infrastructure.Options;

public class AuthenticationOptions
{
    public string Secret { get; set; } = string.Empty;
    public bool ValidateIssuer { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; }
    public bool ValidateAudience { get; set; }
    public string Audience { get; set; } = string.Empty;

    public AuthenticationOptions()
    {

    }
}