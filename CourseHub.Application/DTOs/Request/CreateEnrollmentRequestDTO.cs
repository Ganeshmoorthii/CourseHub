using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Application.DTOs.Request
{
    public class CreateEnrollmentRequestDTO
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }

}
