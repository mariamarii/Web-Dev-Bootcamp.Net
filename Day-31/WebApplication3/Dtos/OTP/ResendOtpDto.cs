using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos.OTP;

public class ResendOtpDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [StringLength(256, ErrorMessage = "Email must not exceed 256 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Purpose is required")]
    [RegularExpression("^(EmailConfirmation|PasswordReset)$", 
        ErrorMessage = "Purpose must be either 'EmailConfirmation' or 'PasswordReset'")]
    public string Purpose { get; set; } = string.Empty;
}
