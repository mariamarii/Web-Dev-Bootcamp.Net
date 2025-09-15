namespace Project.Domain.Models.Base;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}