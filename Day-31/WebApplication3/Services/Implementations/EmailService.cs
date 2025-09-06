using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using WebApplication3.Enum;
using WebApplication3.Helpers;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Services.Implementations;

public class EmailService(
    IOptions<EmailConfig> emailConfig,
    IWebHostEnvironment webHostEnvironment) : IEmailService
{
    private readonly EmailConfig _emailConfig = emailConfig.Value;

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent, string? textContent = null)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlContent;
            
            if (!string.IsNullOrEmpty(textContent))
            {
                bodyBuilder.TextBody = textContent;
            }

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SendTemplatedEmailAsync(string toEmail, EmailTemplateType templateType, Dictionary<string, string> placeholders)
    {
        try
        {
            var template = GetFileBasedTemplate(templateType);
            var subject = ReplacePlaceholders(template.Subject, placeholders);
            var htmlContent = ReplacePlaceholders(template.HtmlContent, placeholders);

            return await SendEmailAsync(toEmail, subject, htmlContent);
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetEmailTemplateAsync(EmailTemplateType templateType, Dictionary<string, string> placeholders)
    {
        var template = GetFileBasedTemplate(templateType);
        return ReplacePlaceholders(template.HtmlContent, placeholders);
    }

    private (string Subject, string HtmlContent) GetFileBasedTemplate(EmailTemplateType templateType)
    {
        return templateType switch
        {
            EmailTemplateType.EmailConfirmation => (
                Subject: "Email Confirmation - {{AppName}}",
                HtmlContent: LoadTemplateFromFile("EmailConfirmation.html")
            ),
            EmailTemplateType.ForgotPassword => (
                Subject: "Password Reset - {{AppName}}",
                HtmlContent: LoadTemplateFromFile("ForgotPassword.html")
            ),
            EmailTemplateType.PasswordChanged => (
                Subject: "Password Changed - {{AppName}}",
                HtmlContent: LoadTemplateFromFile("PasswordChanged.html")
            ),
            EmailTemplateType.Welcome => (
                Subject: "Welcome to {{AppName}}",
                HtmlContent: LoadTemplateFromFile("Welcome.html")
            ),
            _ => (
                Subject: "Notification - {{AppName}}",
                HtmlContent: LoadTemplateFromFile("EmailConfirmation.html")
            )
        };
    }

    private string LoadTemplateFromFile(string fileName)
    {
        try
        {
            var templatePath = Path.Combine(webHostEnvironment.ContentRootPath, "Templates", "Email", fileName);
            return File.ReadAllText(templatePath);
        }
        catch
        {
            return $"<html><body><h1>Email Template</h1><p>Template file not found: {fileName}</p></body></html>";
        }
    }

    private string ReplacePlaceholders(string template, Dictionary<string, string> placeholders)
    {
        foreach (var placeholder in placeholders)
        {
            template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
        }
        return template;
    }
}
