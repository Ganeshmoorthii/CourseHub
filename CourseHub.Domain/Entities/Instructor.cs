using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseHub.Domain.Entities
{
    public class Instructor
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? Bio { get; set; }

        [JsonIgnore]
        public ICollection<Course>? Courses { get; set; }
    }
}
