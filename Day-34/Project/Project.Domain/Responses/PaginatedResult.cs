namespace Project.Domain.Responses;

public record PaginatedResult<Type>(
    IEnumerable<Type> Items,
    int PageNumber,
    int PageSize,
    int TotalRecords,
    int TotalPages);