using System.ComponentModel.DataAnnotations;

namespace CourseHub.Domain.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }
       
        public decimal Price { get; set; }

        public Instructor? Instructor { get; set; }
        public Guid InstructorId { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
