using System.ComponentModel.DataAnnotations;
using WebApplication3.Helpers;

namespace WebApplication3.Attributes;

public class ValidSessionAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string sessionId || string.IsNullOrWhiteSpace(sessionId))
        {
            ErrorMessage = "Session ID is required.";
            return false;
        }

        try
        {
            // We need to access the SessionEncoder service, but since this is a validation attribute,
            // we can't inject dependencies directly. We'll do a basic format validation here
            // and leave the detailed validation to the service layer.
            
            // Basic validation: check if it looks like a base64url encoded string
            if (sessionId.Length < 20) // Minimum reasonable length
            {
                ErrorMessage = "Invalid session format.";
                return false;
            }

            // Check if it contains only valid base64url characters
            var validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
            if (sessionId.Any(c => !validChars.Contains(c)))
            {
                ErrorMessage = "Invalid session format.";
                return false;
            }

            return true;
        }
        catch
        {
            ErrorMessage = "Invalid session format.";
            return false;
        }
    }
}

public class ValidSessionWithServiceAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string sessionId || string.IsNullOrWhiteSpace(sessionId))
        {
            return new ValidationResult("Session ID is required.");
        }

        var sessionEncoder = validationContext.GetService<ISessionEncoder>();
        if (sessionEncoder == null)
        {
            return new ValidationResult("Session validation service not available.");
        }

        try
        {
            var sessionInfo = sessionEncoder.DecodeSession(sessionId);
            if (sessionInfo == null)
            {
                return new ValidationResult("Invalid session format.");
            }

            if (sessionInfo.ExpiresAt < DateTime.UtcNow)
            {
                return new ValidationResult("Session has expired.");
            }

            return ValidationResult.Success;
        }
        catch
        {
            return new ValidationResult("Invalid session format.");
        }
    }
}
