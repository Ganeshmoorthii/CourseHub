using CourseHub.Application.Contracts;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.DTOs.Response;

namespace CourseHub.Application.IServices
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserRequestDTO dto);
        Task<PagedResult<UserSearchDTO>> SearchUsersAsync(UserSearchRequestDTO request);
    }
}
