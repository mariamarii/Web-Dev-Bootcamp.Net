namespace WebApplication3.Dtos.OTP;

public class ConfirmEmailDto
{
    public string SessionId { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
}
