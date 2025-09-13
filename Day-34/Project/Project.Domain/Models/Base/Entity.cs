namespace Project.Domain.Models.Base
{
    public class Entity
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
