using System.ComponentModel.DataAnnotations;
namespace CourseHub.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? LastName { get; set; }
        
        [MaxLength(500)]
        public string? Bio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public User? User { get; set; }
        public Guid UserId { get; set; }
    }
}
