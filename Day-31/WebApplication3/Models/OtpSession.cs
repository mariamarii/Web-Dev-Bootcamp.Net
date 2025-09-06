namespace WebApplication3.Models;

public class OtpSession
{
    public int Id { get; set; }
    public string SessionId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public string OtpCode { get; set; } = null!;
    public string Purpose { get; set; } = null!; 
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
}
