using MediatR;
using WebApplication1.Global;
using System.Text.Json.Serialization;

namespace WebApplication1.Features.Courses.Command.Models;

public class UpdateCourseDto : IRequest<Response>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Hours { get; set; }
}
