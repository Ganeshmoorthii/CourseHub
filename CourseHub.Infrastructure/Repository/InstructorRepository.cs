using CourseHub.Domain.Entities;
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
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ILogger<InstructorRepository> _logger;
        private readonly CourseHubDbContext _dbContext;
        public InstructorRepository(ILogger<InstructorRepository> logger, CourseHubDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task CreateInstructor(Instructor newInstructor)
        {
            await _dbContext.Instructors.AddAsync(newInstructor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
