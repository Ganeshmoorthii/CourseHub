using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Domain.Entities
{
    public class Enrollment
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        public DateTime EnrolledOn { get; set; }
    }
}
