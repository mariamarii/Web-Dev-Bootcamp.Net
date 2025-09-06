namespace WebApplication3.Services.Interfaces;
using WebApplication3.Models;
using WebApplication3.Dtos;
using WebApplication3.Dtos.OTP;

public interface IAuthService
{
    public Task<Response<AuthModal>> Register(RegisterModal model);
    public Task<Response<AuthModal>> Login(LoginModal model);
    public Task<Response<OtpSessionDto>> ForgotPasswordAsync(ForgotPasswordDto model);
    public Task<Response<string>> ChangePasswordAsync(ChangePasswordDto model);
    public Task<Response<string>> ConfirmEmailAsync(ConfirmEmailDto model);
    public Task<Response<OtpSessionDto>> ResendOtpAsync(ResendOtpDto model);
}