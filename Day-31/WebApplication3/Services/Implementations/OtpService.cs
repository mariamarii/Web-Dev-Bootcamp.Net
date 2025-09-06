using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApplication3.Dtos.OTP;
using WebApplication3.Enum;
using WebApplication3.Helpers;
using WebApplication3.Models;
using WebApplication3.Repositories.Interfaces;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Services.Implementations;

public class OtpService(
    IGenericRepository<OtpSession> otpRepository,
    UserManager<User> userManager,
    IEmailService emailService,
    IOptions<OtpConfig> otpConfig,
    ISessionEncoder sessionEncoder) : IOtpService
{
    private readonly OtpConfig _otpConfig = otpConfig.Value;

    public async Task<Response<OtpSessionDto>> GenerateOtpAsync(string userId, string purpose)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new Response<OtpSessionDto>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            var existingSessions = await otpRepository.FindAsync(s => 
                s.UserId == userId && s.Purpose == purpose && !s.IsUsed && s.ExpiresAt > DateTime.UtcNow);
            
            foreach (var session in existingSessions)
            {
                session.IsUsed = true;
                session.UsedAt = DateTime.UtcNow;
                otpRepository.Update(session);
            }

            var otpCode = GenerateOtpCode();
            var expiresAt = DateTime.UtcNow.AddMinutes(_otpConfig.ValidityMinutes);
            var sessionId = sessionEncoder.EncodeSession(userId, expiresAt, purpose);

            var otpSession = new OtpSession
            {
                SessionId = sessionId,
                UserId = userId,
                OtpCode = otpCode,
                Purpose = purpose,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt,
                IsUsed = false
            };

            await otpRepository.AddAsync(otpSession);
            await otpRepository.SaveChangesAsync();

            // Send email
            var templateType = purpose switch
            {
                "EmailConfirmation" => EmailTemplateType.EmailConfirmation,
                "ForgotPassword" => EmailTemplateType.ForgotPassword,
                _ => EmailTemplateType.EmailConfirmation
            };

            var placeholders = new Dictionary<string, string>
            {
                { "UserName", user.UserName ?? user.Email ?? "User" },
                { "OtpCode", otpCode },
                { "ExpiresAt", expiresAt.ToString("yyyy-MM-dd HH:mm:ss UTC") },
                { "AppName", "WebApplication3" }
            };

            await emailService.SendTemplatedEmailAsync(user.Email!, templateType, placeholders);

            return new Response<OtpSessionDto>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "OTP sent successfully",
                Data = new OtpSessionDto
                {
                    SessionId = sessionId,
                    ExpiresAt = expiresAt,
                    Message = $"OTP has been sent to {user.Email}"
                }
            };
        }
        catch (Exception ex)
        {
            return new Response<OtpSessionDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<string>> ValidateOtpAsync(string encodedSessionId, string otpCode, string purpose)
    {
        try
        {
            // First decode and validate the session using the helper
            var sessionInfo = sessionEncoder.DecodeSession(encodedSessionId);
            if (sessionInfo == null)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid session format"
                };
            }

            if (sessionInfo.ExpiresAt < DateTime.UtcNow)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Session has expired"
                };
            }

            if (sessionInfo.Purpose != purpose)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid session purpose"
                };
            }

            var session = await otpRepository.FirstOrDefaultAsync(s => 
                s.SessionId == encodedSessionId && s.Purpose == purpose);

            if (session == null)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Session not found"
                };
            }

            if (session.IsUsed)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "OTP has already been used"
                };
            }

            if (session.ExpiresAt < DateTime.UtcNow)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "OTP has expired"
                };
            }

            if (session.OtpCode != otpCode)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid OTP code"
                };
            }

            session.IsUsed = true;
            session.UsedAt = DateTime.UtcNow;
            otpRepository.Update(session);
            await otpRepository.SaveChangesAsync();

            return new Response<string>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "OTP validated successfully",
                Data = sessionInfo.UserId 
            };
        }
        catch (Exception ex)
        {
            return new Response<string>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<string>> InvalidateSessionAsync(string sessionId)
    {
        try
        {
            var session = await otpRepository.FirstOrDefaultAsync(s => s.SessionId == sessionId);
            
            if (session == null)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Session not found"
                };
            }

            session.IsUsed = true;
            session.UsedAt = DateTime.UtcNow;
            otpRepository.Update(session);
            await otpRepository.SaveChangesAsync();

            return new Response<string>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Session invalidated successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<string>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<OtpSessionDto>> ResendOtpAsync(string email, string purpose)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<OtpSessionDto>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            return await GenerateOtpAsync(user.Id, purpose);
        }
        catch (Exception ex)
        {
            return new Response<OtpSessionDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public string GenerateOtpCode()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);
        var code = Math.Abs(BitConverter.ToInt32(bytes, 0)) % 1000000;
        return code.ToString("D6");
    }
}
