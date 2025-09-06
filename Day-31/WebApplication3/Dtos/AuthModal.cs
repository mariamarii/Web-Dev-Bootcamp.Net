using System.Net;

namespace WebApplication3.Dtos;

public class AuthModal
{
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; }= string.Empty;
    public string Token { get; set; }= string.Empty;
    public string message { get; set; }= string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
    public HttpStatusCode StatusCode { get; set; }

}