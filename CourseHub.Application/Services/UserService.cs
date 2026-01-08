using AutoMapper;
using CourseHub.Application.Contracts;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.DTOs.Response;
using CourseHub.Application.Exceptions;
using CourseHub.Application.IServices;
using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.IRepository;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task CreateUserAsync(CreateUserRequestDTO dto)
    {
        if (dto == null)
            throw new ValidationException("User request cannot be null.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ValidationException("User email is required.");

        if (string.IsNullOrWhiteSpace(dto.UserName))
            throw new ValidationException("User name is required.");

        var user = _mapper.Map<User>(dto);
        await _userRepository.AddUserAsync(user);
    }

    public async Task<PagedResult<UserSearchDTO>> SearchUsersAsync(
     UserSearchRequestDTO request)
    {
        if (request.Page <= 0 || request.PageSize <= 0)
            throw new ValidationException("Invalid pagination values.");

        var (users, totalCount) =
            await _userRepository.SearchUsersAsync(request);

        var items = users.Select(user => new UserSearchDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Profile = user.Profile == null ? null : new UserProfileInfoDTO
            {
                FullName = $"{user.Profile.FirstName} {user.Profile.LastName}",
                Bio = user.Profile.Bio,
                DateOfBirth = user.Profile.DateOfBirth
            },
            Enrollments = user.Enrollments.Select(e => new EnrollmentInfoDTO
            {
                EnrolledAt = e.EnrolledAt,
                Status = e.Status,
                Course = new CourseInfoDTO
                {
                    Id = e.Course.Id,
                    Title = e.Course.Title,
                    Price = e.Course.Price,
                    InstructorId = e.Course.InstructorId
                }
            }).ToList()
        }).ToList();

        return new PagedResult<UserSearchDTO>(
            items,
            totalCount,
            request.Page,
            request.PageSize
        );
    }
}
