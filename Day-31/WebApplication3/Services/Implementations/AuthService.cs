using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApplication3.Models;
using WebApplication3.Dtos;
using WebApplication3.Dtos.OTP;
using WebApplication3.Helpers;
using WebApplication3.Services.Interfaces;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication3.Enum;

namespace WebApplication3.Services.Implementations;

public class AuthService(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IMapper mapper,
    IOptions<JWT> jwt,
    IOtpService otpService,
    IEmailService emailService) : IAuthService
{
    private readonly JWT _jwt = jwt.Value;

    public async Task<Response<AuthModal>> Login(LoginModal model)
    {
        var user = await userManager.FindByNameAsync(model.UserName); 
        if (user is null)
        {
            return new Response<AuthModal>
            {
                Status = false,
                Message = "Invalid credentials",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
        if (!passwordValid)
        {
            return new Response<AuthModal>
            {
                Status = false,
                Message = "Invalid credentials",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        // Check if email is confirmed
        if (!user.EmailConfirmed)
        {
            return new Response<AuthModal>
            {
                Status = false,
                Message = "Email not confirmed. Please confirm your email before logging in.",
                StatusCode = HttpStatusCode.Forbidden
            };
        }

        var jwtSecurityToken = await CreateJwtToken(user);

        return new Response<AuthModal>
        {
            Status = true,
            StatusCode = HttpStatusCode.OK,
            Data = new AuthModal
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty,
                UserName = user.UserName ?? string.Empty
            },
            Message = "Login successful"
        };
    }

    public async Task<Response<AuthModal>> Register(RegisterModal model)
    {
        if (await userManager.FindByEmailAsync(model.Email) is not null)
        {
            return new Response<AuthModal>
            {
                Status = false,
                Message = "Email already exists",
                StatusCode = HttpStatusCode.Conflict
            };
        }
        var user = mapper.Map<User>(model);

        var result = await userManager.CreateAsync(user, model.Password);

        await userManager.AddToRoleAsync(user , model.Role);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(x => x.Description));
            return new Response<AuthModal>
            {
                Status = false,
                Message = errors,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        // Generate OTP for email confirmation instead of returning token immediately
        var otpResult = await otpService.GenerateOtpAsync(user.Id, "EmailConfirmation");
        
        if (!otpResult.Status)
        {
            return new Response<AuthModal>
            {
                Status = false,
                Message = "Failed to send confirmation email. Please try again.",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        return new Response<AuthModal>
        {
            Status = true,
            Message = "User created successfully. Please check your email for confirmation code.",
            StatusCode = HttpStatusCode.Created,
            Data = new AuthModal
            {
                Token = string.Empty, // No token until email is confirmed
                Role = model.Role,
                UserName = user.UserName ?? string.Empty,
                SessionId = otpResult.Data?.SessionId ?? string.Empty,
                ExpiresAt = otpResult.Data?.ExpiresAt,
                message = $"Confirmation email sent. Code expires at {otpResult.Data?.ExpiresAt:yyyy-MM-dd HH:mm:ss}"
            }
        };
    }

    private async Task<JwtSecurityToken> CreateJwtToken(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name , user.UserName ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier , user.Id),
            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role , roles.FirstOrDefault() ?? string.Empty)
        }
        .Union(userClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey , SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.ExpireDays),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    public async Task<Response<OtpSessionDto>> ForgotPasswordAsync(ForgotPasswordDto model)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new Response<OtpSessionDto>
                {
                    Status = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "If the email exists in our system, you will receive a password reset email.",
                    Data = new OtpSessionDto
                    {
                        SessionId = "",
                        ExpiresAt = DateTime.UtcNow,
                        Message = "Password reset email sent if email exists."
                    }
                };
            }

            return await otpService.GenerateOtpAsync(user.Id, "ForgotPassword");
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

    public async Task<Response<string>> ChangePasswordAsync(ChangePasswordDto model)
    {
        try
        {
            var validationResult = await otpService.ValidateOtpAsync(model.SessionId, model.OtpCode, "ForgotPassword");
            if (!validationResult.Status)
            {
                return validationResult;
            }

            var user = await userManager.FindByIdAsync(validationResult.Data!);
            if (user == null)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            var removePasswordResult = await userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = string.Join(", ", removePasswordResult.Errors.Select(e => e.Description))
                };
            }

            var addPasswordResult = await userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = string.Join(", ", addPasswordResult.Errors.Select(e => e.Description))
                };
            }

            var placeholders = new Dictionary<string, string>
            {
                { "UserName", user.UserName ?? user.Email ?? "User" },
                { "DateTime", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC") },
                { "AppName", "WebApplication3" }
            };

            await emailService.SendTemplatedEmailAsync(user.Email!, EmailTemplateType.PasswordChanged, placeholders);

            return new Response<string>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Password changed successfully"
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

    public async Task<Response<string>> ConfirmEmailAsync(ConfirmEmailDto model)
    {
        try
        {
            var validationResult = await otpService.ValidateOtpAsync(model.SessionId, model.OtpCode, "EmailConfirmation");
            if (!validationResult.Status)
            {
                return validationResult;
            }

            var user = await userManager.FindByIdAsync(validationResult.Data!);
            if (user == null)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            user.EmailConfirmed = true;
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new Response<string>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            var placeholders = new Dictionary<string, string>
            {
                { "UserName", user.UserName ?? user.Email ?? "User" },
                { "AppName", "WebApplication3" }
            };

            await emailService.SendTemplatedEmailAsync(user.Email!, EmailTemplateType.Welcome, placeholders);

            return new Response<string>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Email confirmed successfully"
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

    public async Task<Response<OtpSessionDto>> ResendOtpAsync(ResendOtpDto model)
    {
        try
        {
            return await otpService.ResendOtpAsync(model.Email, model.Purpose);
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
}
