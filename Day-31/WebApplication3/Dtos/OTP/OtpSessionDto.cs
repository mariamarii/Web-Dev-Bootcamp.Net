namespace WebApplication3.Dtos.OTP;

public class OtpSessionDto
{
    public string SessionId { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string Message { get; set; } = string.Empty;
}
