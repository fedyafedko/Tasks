using Microsoft.AspNetCore.Identity;

namespace Tasks.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
