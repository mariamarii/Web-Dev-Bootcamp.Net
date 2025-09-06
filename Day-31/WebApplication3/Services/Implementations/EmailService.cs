using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using WebApplication3.Enum;
using WebApplication3.Helpers;
using WebApplication3.Models;
using WebApplication3.Repositories.Interfaces;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Services.Implementations;

public class EmailService(
    IOptions<EmailConfig> emailConfig,
    IGenericRepository<EmailTemplate> templateRepository,
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
            // Log the exception
            Console.WriteLine($"Failed to send email: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SendTemplatedEmailAsync(string toEmail, EmailTemplateType templateType, Dictionary<string, string> placeholders)
    {
        try
        {
            var template = await templateRepository.FirstOrDefaultAsync(t => 
                t.TemplateName == templateType.ToString() && t.IsActive);

            if (template == null)
            {
                // Fallback to default template
                template = GetDefaultTemplate(templateType);
            }

            var subject = ReplacePlaceholders(template.Subject, placeholders);
            var htmlContent = ReplacePlaceholders(template.HtmlContent, placeholders);
            var textContent = ReplacePlaceholders(template.TextContent, placeholders);

            return await SendEmailAsync(toEmail, subject, htmlContent, textContent);
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetEmailTemplateAsync(EmailTemplateType templateType, Dictionary<string, string> placeholders)
    {
        var template = await templateRepository.FirstOrDefaultAsync(t => 
            t.TemplateName == templateType.ToString() && t.IsActive);

        if (template == null)
        {
            template = GetDefaultTemplate(templateType);
        }

        return ReplacePlaceholders(template.HtmlContent, placeholders);
    }

    private string ReplacePlaceholders(string content, Dictionary<string, string> placeholders)
    {
        foreach (var placeholder in placeholders)
        {
            content = content.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
        }
        return content;
    }

    private EmailTemplate GetDefaultTemplate(EmailTemplateType templateType)
    {
        return templateType switch
        {
            EmailTemplateType.EmailConfirmation => new EmailTemplate
            {
                Subject = "Confirm Your Email Address",
                HtmlContent = LoadTemplateFromFile("EmailConfirmation.html"),
                TextContent = "Please confirm your email using this OTP: {{OtpCode}}. This code expires at {{ExpiresAt}}."
            },
            EmailTemplateType.ForgotPassword => new EmailTemplate
            {
                Subject = "Reset Your Password",
                HtmlContent = LoadTemplateFromFile("ForgotPassword.html"),
                TextContent = "Use this OTP to reset your password: {{OtpCode}}. This code expires at {{ExpiresAt}}."
            },
            EmailTemplateType.PasswordChanged => new EmailTemplate
            {
                Subject = "Password Changed Successfully",
                HtmlContent = LoadTemplateFromFile("PasswordChanged.html"),
                TextContent = "Your password has been changed successfully on {{DateTime}}."
            },
            EmailTemplateType.Welcome => new EmailTemplate
            {
                Subject = "Welcome to {{AppName}}",
                HtmlContent = LoadTemplateFromFile("Welcome.html"),
                TextContent = "Welcome {{UserName}}! Thank you for joining {{AppName}}."
            },
            _ => throw new ArgumentException("Unknown template type")
        };
    }

    private string LoadTemplateFromFile(string fileName)
    {
        try
        {
            var templatePath = Path.Combine(webHostEnvironment.ContentRootPath, "Templates", "Email", fileName);
            if (File.Exists(templatePath))
            {
                return File.ReadAllText(templatePath);
            }
            
            // Return a simple fallback if file doesn't exist
            return GetFallbackTemplate(fileName);
        }
        catch
        {
            // Return a simple fallback on error
            return GetFallbackTemplate(fileName);
        }
    }

    private string GetFallbackTemplate(string fileName)
    {
        var templateName = Path.GetFileNameWithoutExtension(fileName);
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #f5f5f5; }}
        .container {{ max-width: 600px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>{templateName} Template</h1>
        <p>Template file not found. Please contact administrator.</p>
    </div>
</body>
</html>";
    }
}
