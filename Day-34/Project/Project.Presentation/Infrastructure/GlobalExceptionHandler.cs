using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Project.Presentation.Infrastructure;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred: {ExceptionType}", exception.GetType().Name);

        var (statusCode, title, detail) = GetErrorDetails(exception);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Type = GetErrorType(statusCode),
            Title = title,
            Detail = detail
        };
        // var response = new Response<object>(null, title, false, (HttpStatusCode)statusCode);

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static (int statusCode, string title, string? detail) GetErrorDetails(Exception exception)
    {
        return exception switch
        {
            // Database exceptions - handle first as they're most specific
            DbUpdateException dbUpdateEx => HandleDbUpdateException(dbUpdateEx),
            
            // SQL exceptions
            SqlException sqlEx => HandleSqlException(sqlEx),
            
            // Validation exceptions
            ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                "Validation failed",
                validationEx.Message
            ),
            
            // Business rule validation exceptions
            // BusinessRuleValidationException businessEx => (
            //     StatusCodes.Status400BadRequest,
            //     "Business rule violation",
            //     businessEx.Message
            // ),
            
            ArgumentOutOfRangeException argRangeEx => (
                StatusCodes.Status400BadRequest,
                "Argument out of range",
                argRangeEx.Message
            ),
            
            ArgumentNullException argNullEx => (
                StatusCodes.Status400BadRequest,
                "Required argument is null",
                argNullEx.Message
            ),
            
            ArgumentException argEx => (
                StatusCodes.Status400BadRequest,
                "Invalid argument",
                argEx.Message
            ),
            
            // Unauthorized exceptions
            UnauthorizedAccessException unauthorizedEx => (
                StatusCodes.Status401Unauthorized,
                "Unauthorized access",
                unauthorizedEx.Message
            ),
            
            // Forbidden exceptions
            InvalidOperationException invalidOpEx when invalidOpEx.Message.Contains("permission", StringComparison.OrdinalIgnoreCase) => (
                StatusCodes.Status403Forbidden,
                "Access forbidden",
                invalidOpEx.Message
            ),
            
            // Not found exceptions
            KeyNotFoundException keyNotFoundEx => (
                StatusCodes.Status404NotFound,
                "Resource not found",
                keyNotFoundEx.Message
            ),
            
            InvalidOperationException notFoundEx when notFoundEx.Message.Contains("not found", StringComparison.OrdinalIgnoreCase) => (
                StatusCodes.Status404NotFound,
                "Resource not found",
                notFoundEx.Message
            ),
            
            // Conflict exceptions
            InvalidOperationException conflictEx when conflictEx.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase) => (
                StatusCodes.Status409Conflict,
                "Resource conflict",
                conflictEx.Message
            ),
            
            // Too many requests
            InvalidOperationException rateLimitEx when rateLimitEx.Message.Contains("rate limit", StringComparison.OrdinalIgnoreCase) => (
                StatusCodes.Status429TooManyRequests,
                "Rate limit exceeded",
                rateLimitEx.Message
            ),
            
            // Default case - internal server error
            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred",
                "Please try again later or contact support if the problem persists."
            )
        };
    }

    private static (int statusCode, string title, string detail) HandleDbUpdateException(DbUpdateException dbUpdateEx)
    {
        // Check if it's a constraint violation
        if (dbUpdateEx.InnerException is SqlException sqlEx)
        {
            return HandleSqlException(sqlEx);
        }

        // Generic database update error
        return (
            StatusCodes.Status500InternalServerError,
            "Database update failed",
            "An error occurred while saving data. Please try again or contact support if the problem persists."
        );
    }

    private static (int statusCode, string title, string detail) HandleSqlException(SqlException sqlEx)
    {
        return sqlEx.Number switch
        {
            // Primary key violation
            2627 => (
                StatusCodes.Status409Conflict,
                "Duplicate entry",
                "A record with this information already exists. Please use different values."
            ),
            
            // Unique constraint violation
            2601 => (
                StatusCodes.Status409Conflict,
                "Duplicate entry",
                "A record with this information already exists. Please use different values."
            ),
            
            // Foreign key constraint violation
            547 => (
                StatusCodes.Status400BadRequest,
                "Invalid reference",
                "The operation cannot be completed because it references data that doesn't exist."
            ),
 
            // Deadlock
            1205 => (
                StatusCodes.Status409Conflict,
                "Database conflict",
                "The operation could not be completed due to a database conflict. Please try again."
            ),
            
            // Timeout
            -2 => (
                StatusCodes.Status408RequestTimeout,
                "Database timeout",
                "The database operation timed out. Please try again."
            ),
            
            // Connection failure
            53 => (
                StatusCodes.Status503ServiceUnavailable,
                "Database unavailable",
                "The database is currently unavailable. Please try again later."
            ),
            
            // Login failed
            18456 => (
                StatusCodes.Status500InternalServerError,
                "Database authentication error",
                "A database authentication error occurred. Please contact support."
            ),
            
            // Permission denied
            229 => (
                StatusCodes.Status403Forbidden,
                "Database permission denied",
                "You don't have permission to perform this database operation."
            ),
            
            // Default SQL error
            _ => (
                StatusCodes.Status500InternalServerError,
                "Database error",
                $"A database error occurred (Error {sqlEx.Number}). Please contact support if this problem persists."
            )
        };
    }

    private static string GetErrorType(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            StatusCodes.Status401Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            StatusCodes.Status403Forbidden => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            StatusCodes.Status408RequestTimeout => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.7",
            StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            StatusCodes.Status429TooManyRequests => "https://datatracker.ietf.org/doc/html/rfc6585#section-4",
            StatusCodes.Status500InternalServerError => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            StatusCodes.Status503ServiceUnavailable => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.4",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
    }
}
