namespace WebApplication3.Services.Interfaces;
using WebApplication3.Models;
using WebApplication3.Dtos;
public interface IAuthService
{
    public Task<Response<AuthModal>> Register(RegisterModal model);
    public Task<Response<AuthModal>> Login(LoginModal model);
}