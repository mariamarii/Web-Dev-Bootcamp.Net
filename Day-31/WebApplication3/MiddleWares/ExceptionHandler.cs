using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using WebApplication3.Models;
using System.Net;

namespace WebApplication3.MiddleWares;

public class ExceptionHandler
{
     private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
               
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string> { Status = false, Message = error?.Message };
                _logger.LogError(error, error.Message, context.Request, "");

                switch (error)
                {
                    case UnauthorizedAccessException e:
                        // custom application error
                        responseModel.Message = error.Message;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ValidationException e:
                        // custom validation error
                        responseModel.Message = error.Message;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        responseModel.Message = error.Message;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case Exception e:
                        if (e.GetType().ToString() == "ApiException")
                        {
                            responseModel.Message += e.Message;
                            responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        responseModel.Message = e.Message;
                        responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                    default:
                        // unhandled error
                        responseModel.Message = error.Message;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
