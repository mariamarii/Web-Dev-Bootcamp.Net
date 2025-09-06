namespace WebApplication3.Dtos.OTP;

public class ChangePasswordDto
{
    public string SessionId { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
