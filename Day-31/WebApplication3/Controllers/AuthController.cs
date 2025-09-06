using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Controllers.Base;
using WebApplication3.Dtos;
using WebApplication3.Dtos.OTP;
using WebApplication3.Models;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : BaseController
{

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModal model)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<object>
                {
                    Data = ModelState,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var result = await authService.Register(model);
            return Result(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModal model)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<object>
                {
                    Data = ModelState,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var result = await authService.Login(model);
            return Result(result);
        }

        [Authorize]
        [HttpGet]
        [Route("test")]
        public IActionResult TestAuthorize()
        {
            return Result(new Response<string>
            {
                Data = "Authorized",
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<string>
                {
                    Message = "Invalid input",
                    Data = "Model validation failed",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var result = await authService.ConfirmEmailAsync(confirmEmailDto);
            return Result(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<string>
                {
                    Message = "Invalid email format",
                    Data = "Model validation failed",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var result = await authService.ForgotPasswordAsync(forgotPasswordDto);
            return Result(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<string>
                {
                    Message = "Invalid input",
                    Data = "Model validation failed",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var result = await authService.ChangePasswordAsync(changePasswordDto);
            return Result(result);
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpDto resendOtpDto)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<string>
                {
                    Message = "Invalid input",
                    Data = "Model validation failed",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var result = await authService.ResendOtpAsync(resendOtpDto);
            return Result(result);
        }
    }


