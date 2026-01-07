using CourseHub.Infrastructure.Data;
using CourseHub.Infrastructure.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ILogger<CourseRepository> _logger;
        private readonly CourseHubDbContext _dbContext;
        public CourseRepository(ILogger<CourseRepository> logger, CourseHubDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task AddCourseAsync(Domain.Entities.Course course)
        {
            _logger.LogInformation("AddCourseAsync method called in CourseRepository.");
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Course added to the database successfully.");
        }
        public async Task<bool> ExistsAsync(Guid courseId)
        {
            _logger.LogInformation("ExistsAsync method called in CourseRepository.");
            var exists = await Task.FromResult(_dbContext.Courses.Any(c => c.Id == courseId));
            _logger.LogInformation($"Course existence check completed: {exists}");
            return exists;
        }
    }
}
