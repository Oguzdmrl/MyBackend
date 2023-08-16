namespace Entities.JWTEntity;

public sealed class JwtSettings
{
    public JwtSettings(string issuer, string audience, string signingKey)
    {
        Issuer = issuer;
        Audience = audience;
        SigningKey = signingKey;
    }
    public JwtSettings()
    {
    }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
}