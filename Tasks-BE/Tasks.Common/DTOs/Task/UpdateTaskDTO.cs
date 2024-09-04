using Tasks.Entities.Enums;

namespace Tasks.Common.DTOs.Task
{
    public class UpdateTaskDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
