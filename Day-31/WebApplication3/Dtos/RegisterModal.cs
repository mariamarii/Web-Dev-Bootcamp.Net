namespace WebApplication3.Dtos;

public class RegisterModal
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; }  = string.Empty;
    public string Email { get; set; } = string.Empty;
}