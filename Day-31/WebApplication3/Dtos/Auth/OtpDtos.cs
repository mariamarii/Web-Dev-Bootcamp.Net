using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos.Auth;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}

public class ChangePasswordDto
{
    [Required]
    public string SessionId { get; set; } = null!;
    
    [Required]
    [StringLength(6, MinimumLength = 6)]
    public string OtpCode { get; set; } = null!;
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; } = null!;
    
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = null!;
}

public class ConfirmEmailDto
{
    [Required]
    public string SessionId { get; set; } = null!;
    
    [Required]
    [StringLength(6, MinimumLength = 6)]
    public string OtpCode { get; set; } = null!;
}

public class ResendOtpDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    public string Purpose { get; set; } = null!; // EmailConfirmation or ForgotPassword
}
