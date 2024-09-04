using System.ComponentModel.DataAnnotations.Schema;
using Tasks.Entities.Enums;

namespace Tasks.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public Status Status { get; set; }
        
        public Priority Priority { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdateAt { get; set; }
        
        [ForeignKey(nameof(User))]        
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
