using WebApplication3.Dtos.OTP;
using WebApplication3.Models;

namespace WebApplication3.Services.Interfaces;

public interface IOtpService
{
    Task<Response<OtpSessionDto>> GenerateOtpAsync(string userId, string purpose);
    Task<Response<string>> ValidateOtpAsync(string sessionId, string otpCode, string purpose);
    Task<Response<string>> InvalidateSessionAsync(string sessionId);
    Task<Response<OtpSessionDto>> ResendOtpAsync(string email, string purpose);
}
