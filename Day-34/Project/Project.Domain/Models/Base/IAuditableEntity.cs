namespace Project.Domain.Models.Base;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}