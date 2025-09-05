using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Controllers.Base;
using WebApplication3.Dtos;
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
    }


