using System.ComponentModel.DataAnnotations;

namespace Tasks.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
