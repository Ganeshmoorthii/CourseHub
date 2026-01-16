using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Application.DTOs.Request
{
    public class UserSearchRequestDTO
    {
        public Guid? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        public string? InstructorName { get; set; }
        public string? CourseTitle { get; set; }

        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public DateTime? EnrolledFrom { get; set; }
        public DateTime? EnrolledTo { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? OrderByData { get; set; } = "UserName";
    }

}
