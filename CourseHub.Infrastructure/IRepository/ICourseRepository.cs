using CourseHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Infrastructure.IRepository
{
    public interface ICourseRepository
    {
        public Task AddCourseAsync(Course course);
        public Task<bool> ExistsAsync(Guid courseId);
    }
}
