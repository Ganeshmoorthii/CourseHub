using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.Data;
using CourseHub.Infrastructure.IRepository;
using Microsoft.Extensions.Logging;

namespace CourseHub.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly CourseHubDbContext _dbContext;
        public UserRepository(ILogger<UserRepository> logger, CourseHubDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task AddUserAsync(User newUser)
        {
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(Guid userId)
        {
            var exists = await Task.FromResult(_dbContext.Users.Any(u => u.Id == userId));
            return exists;
        }
    }
}
