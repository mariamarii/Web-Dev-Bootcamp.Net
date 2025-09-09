using System.ComponentModel.DataAnnotations;
using WebApplication1.Global;

namespace WebApplication1.Middlewares;

using System.Net;
using System.Text.Json;


public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseModel = new Response()
            {
                Status = false,
                Message = error?.Message ?? "An error occurred"
            };

            logger.LogError(error, "Exception caught in middleware");

            switch (error)
            {
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case ValidationException:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            responseModel.StatusCode = (HttpStatusCode)response.StatusCode;

            var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(result);
        }
    }
}

