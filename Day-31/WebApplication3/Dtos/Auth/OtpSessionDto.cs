namespace WebApplication3.Dtos.Auth;

public class OtpSessionDto
{
    public string SessionId { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public string Message { get; set; } = null!;
}
