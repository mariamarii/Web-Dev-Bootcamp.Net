using System.ComponentModel.DataAnnotations;
using WebApplication3.Attributes;

namespace WebApplication3.Dtos.OTP;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Session ID is required.")]
    [ValidSessionWithService(ErrorMessage = "Invalid or expired session.")]
    public string SessionId { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "OTP code is required.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP code must be exactly 6 digits.")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP code must contain only digits.")]
    public string OtpCode { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "New password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public string NewPassword { get; set; } = string.Empty;
}
