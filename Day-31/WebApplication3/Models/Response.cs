using System.Net;

namespace WebApplication3.Models;

public class Response<T>
{
    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }
    public T? Data { get; set; }
    
}