using System.Net;

namespace WebApplication1.Global;

public class Response
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public Response(object? data = null, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Status = statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created;
        Message = message ?? statusCode.ToString();
        Data = data;
        StatusCode = statusCode;
    }
}