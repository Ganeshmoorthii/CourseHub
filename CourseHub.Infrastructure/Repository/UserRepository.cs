using CourseHub.Application.DTOs.Request;
using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.Data;
using CourseHub.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly CourseHubDbContext _dbContext;

    public UserRepository(CourseHubDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task AddUserAsync(User newUser)
    {
        if (newUser == null)
            throw new ArgumentNullException(nameof(newUser));

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

    public async Task<(List<User> Users, int TotalCount)> SearchUsersAsync(UserSearchRequestDTO request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var query = _dbContext.Users
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
                    .ThenInclude(c => c.Instructor)
            .AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(u => u.Id == request.Id);

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
                u.Enrollments != null && u.Enrollments.Any(e =>
                    e.Course != null && e.Course.Title.Contains(request.CourseTitle)));

        if (!string.IsNullOrWhiteSpace(request.InstructorName))
            query = query.Where(u =>
                u.Enrollments != null && u.Enrollments.Any(e =>
                    e.Course != null && e.Course.Instructor != null && 
                    e.Course.Instructor.Name.Contains(request.InstructorName)));

        if (request.PriceFrom.HasValue)
            query = query.Where(u =>
                u.Enrollments != null && u.Enrollments.Any(e => 
                    e.Course != null && e.Course.Price >= request.PriceFrom));

        if (request.PriceTo.HasValue)
            query = query.Where(u =>
                u.Enrollments != null && u.Enrollments.Any(e => 
                    e.Course != null && e.Course.Price <= request.PriceTo));

        if (request.EnrolledFrom.HasValue)
            query = query.Where(u =>
                u.Enrollments != null && u.Enrollments.Any(e => 
                    e.EnrolledAt >= request.EnrolledFrom));

        if (request.EnrolledTo.HasValue)
            query = query.Where(u =>
                u.Enrollments != null && u.Enrollments.Any(e => 
                    e.EnrolledAt <= request.EnrolledTo));

        query = ApplyOrderBy(query, request.OrderByData);

        var totalCount = await query.CountAsync();

        var users = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return (users, totalCount);
    }

     /// <summary>
    /// Applies dynamic ordering to the query based on the orderBy parameter.
    /// Uses a projection to compute a stable ordering key that EF Core can translate to SQL.
    /// </summary>
    private IQueryable<User> ApplyOrderBy(IQueryable<User> query, string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return query.OrderBy(u => u.UserName);

        switch (orderBy.Trim().ToLowerInvariant())
        {
            case "username":
                return query.OrderBy(u => u.UserName);

            case "email":
                return query.OrderBy(u => u.Email);

            case "dateofbirth":
                return query
                    .Select(u => new { User = u, Key = (DateOnly?)u.Profile!.DateOfBirth })
                    .OrderBy(x => x.Key)
                    .Select(x => x.User);

            case "price":
                return query
                    .Select(u => new
                    {
                        User = u,
                        Key = (decimal?)u.Enrollments!.Min(e => e.Course!.Price)
                    })
                    .OrderBy(x => x.Key ?? 0m)
                    .Select(x => x.User);

            case "enrolledat":
                return query
                    .Select(u => new
                    {
                        User = u,
                        Key = (DateTime?)u.Enrollments!.Min(e => e.EnrolledAt)
                    })
                    .OrderBy(x => x.Key ?? DateTime.MinValue)
                    .Select(x => x.User);

            case "instructorname":
                return query
                    .Select(u => new
                    {
                        User = u,
                        Key = u.Enrollments!.Min(e => e.Course!.Instructor!.Name)
                    })
                    .OrderBy(x => x.Key ?? string.Empty)
                    .Select(x => x.User);

            default:
                return query.OrderBy(u => u.UserName);
        }
    }
}
