using CourseHub.Application.DTOs.Request;
using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.Data;
using CourseHub.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly CourseHubDbContext _dbContext;

    public UserRepository(CourseHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserAsync(User newUser)
    {
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid userId)
    {
        return await _dbContext.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserWithProfileAndEnrollmentsAsync(Guid userId)
    {
        return await _dbContext.Users
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
                    .ThenInclude(c => c.Instructor)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<(List<User> Users, int TotalCount)> SearchUsersAsync(
    UserSearchRequestDTO request)
    {
        var query = _dbContext.Users
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
                    .ThenInclude(c => c.Instructor)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.UserName))
            query = query.Where(u => u.UserName.Contains(request.UserName));

        if (!string.IsNullOrWhiteSpace(request.Email))
            query = query.Where(u => u.Email.Contains(request.Email));

        if (request.DateOfBirth.HasValue)
        {
            var dob = request.DateOfBirth.Value;
            query = query.Where(u => u.Profile != null && u.Profile.DateOfBirth == dob);
        }

        if (!string.IsNullOrWhiteSpace(request.CourseTitle))
            query = query.Where(u =>
                u.Enrollments.Any(e =>
                    e.Course.Title.Contains(request.CourseTitle)));

        if (!string.IsNullOrWhiteSpace(request.InstructorName))
            query = query.Where(u =>
                u.Enrollments.Any(e =>
                    e.Course.Instructor.Name.Contains(request.InstructorName)));

        if (request.PriceFrom.HasValue)
            query = query.Where(u =>
                u.Enrollments.Any(e => e.Course.Price >= request.PriceFrom));

        if (request.PriceTo.HasValue)
            query = query.Where(u =>
                u.Enrollments.Any(e => e.Course.Price <= request.PriceTo));

        if (request.EnrolledFrom.HasValue)
            query = query.Where(u =>
                u.Enrollments.Any(e => e.EnrolledAt >= request.EnrolledFrom));

        if (request.EnrolledTo.HasValue)
            query = query.Where(u =>
                u.Enrollments.Any(e => e.EnrolledAt <= request.EnrolledTo));

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderBy(u => u.UserName)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return (users, totalCount);
    }
}
