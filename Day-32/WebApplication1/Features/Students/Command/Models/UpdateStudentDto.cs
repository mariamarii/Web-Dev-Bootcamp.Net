using MediatR;
using WebApplication1.Global;
using System.Text.Json.Serialization;

namespace WebApplication1.Features.Students.Command.Models;

public class UpdateStudentDto : IRequest<Response>
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}