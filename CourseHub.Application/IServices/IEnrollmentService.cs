using CourseHub.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Application.IServices
{
    public interface IEnrollmentService
    {
        public Task CreateEnrollmentAsync(CreateEnrollmentRequestDTO dto);
    }
}
