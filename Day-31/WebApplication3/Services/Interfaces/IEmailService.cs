using WebApplication3.Enum;

namespace WebApplication3.Services.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent, string? textContent = null);
    Task<bool> SendTemplatedEmailAsync(string toEmail, EmailTemplateType templateType, Dictionary<string, string> placeholders);
    Task<string> GetEmailTemplateAsync(EmailTemplateType templateType, Dictionary<string, string> placeholders);
}
