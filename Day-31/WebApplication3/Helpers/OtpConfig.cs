namespace WebApplication3.Helpers;

public class OtpConfig
{
    public string EncryptionKey { get; set; } = "MyDefaultSecretKey123456789012"; 
    public int ValidityMinutes { get; set; } = 15;
}
