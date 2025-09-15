namespace Project.Domain.Filters;

public record BaseFilter(int PageSize = 10, int PageNumber = 1);