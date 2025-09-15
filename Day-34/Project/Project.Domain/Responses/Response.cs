using System.Net;

namespace Project.Domain.Responses;

public record Response<Type>
(
    Type? Data,
    string Message,
    bool Status,
    HttpStatusCode StatusCode
)
{

    public static Response<PaginatedResult<Type>> GetData(IEnumerable<Type> data, int pageNumber, int pageSize, int totalRecords)
    {
        var pagedData = new PaginatedResult<Type>
        (
            data,
            pageNumber,
            pageSize,
            totalRecords,
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return new Response<PaginatedResult<Type>>(pagedData, "Data retrieved successfully", true, HttpStatusCode.OK);
    }
    
    public static Response<Type> Success(Type? data = default)
    {
        return new Response<Type>(data, "Request was successful", true, HttpStatusCode.OK);
    }
    
    public static Response<Type> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Response<Type>(default!, message, false, statusCode);
    }
    
    public static Response<Type> NotFound(string message)
    {
        return new Response<Type>(default!, message, false, HttpStatusCode.NotFound);
    }
    
    public static Response<Type> Created(Type data)
    {
        return new Response<Type>(data, "Resource created successfully", true, HttpStatusCode.Created);
    }
    
    public static Response<Type> Unauthorized(string message)
    {
        return new Response<Type>(default!, message, false, HttpStatusCode.Unauthorized);
    }
}