using CourseHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Application.DTOs.Request
{
    public class CreateCourseRequestDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        //public Instructor? Instructor { get; set; }
        public Guid InstructorId { get; set; }
    }
}
