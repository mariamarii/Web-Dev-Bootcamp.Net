using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApplication3.Models;
using WebApplication3.Dtos;
using WebApplication3.Helpers;
using WebApplication3.Services.Interfaces;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication3.Services.Implementations;

public class AuthService(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IMapper mapper,
    IOptions<JWT> jwt) : IAuthService
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

            var jwtSecurityToken = await CreateJwtToken(user);

            return new Response<AuthModal>
            {
                Status = true,
                Message = "User created successfully",
                StatusCode = HttpStatusCode.OK,
                Data = new AuthModal
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Role = model.Role,
                    UserName = user.UserName ?? string.Empty
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
}