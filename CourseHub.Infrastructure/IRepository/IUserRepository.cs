using CourseHub.Domain.Entities;
using CourseHub.Application.DTOs.Request;

namespace CourseHub.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        Task AddUserAsync(User newUser);
        Task<bool> ExistsAsync(Guid userId);
        Task<User?> GetUserWithProfileAndEnrollmentsAsync(Guid userId);

        Task<(List<User> Users, int TotalCount)> SearchUsersAsync(
            UserSearchRequestDTO request);
    }
}
