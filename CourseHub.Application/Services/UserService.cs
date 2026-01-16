using AutoMapper;
using CourseHub.Application.Contracts;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.DTOs.Response;
using CourseHub.Application.Exceptions;
using CourseHub.Application.IServices;
using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.IRepository;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    private const string SEARCH_CACHE_KEY_PREFIX = "user_search_";
    private const int CACHE_DURATION_MINUTES = 10;

    public UserService(IUserRepository userRepository, IMapper mapper, IMemoryCache memoryCache)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
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

    public async Task<PagedResult<UserSearchDTO>> SearchUsersAsync(UserSearchRequestDTO request)
    {
        if (request.Page <= 0 || request.PageSize <= 0)
            throw new ValidationException("Invalid pagination values.");

        var cacheKey = GenerateCacheKey(request);

        if (_memoryCache.TryGetValue(cacheKey, out PagedResult<UserSearchDTO>? cachedResult))
        {
            return cachedResult!;
        }

        var (users, totalCount) = await _userRepository.SearchUsersAsync(request);

        var items = users.Select(user => new UserSearchDTO
        {
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

        var result = new PagedResult<UserSearchDTO>(
            items,
            totalCount,
            request.Page,
            request.PageSize
        );

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));

        _memoryCache.Set(cacheKey, result, cacheOptions);

        return result;
    }

    /// <summary>
    /// Generates a unique cache key based on search request parameters.
    /// Uses SHA256 to create a consistent hash of the serialized request.
    /// </summary>
    private string GenerateCacheKey(UserSearchRequestDTO request)
    {
        return string.Join("|",
            "user_search",
            $"id={request.Id?.ToString() ?? "null"}",
            $"username={Normalize(request.UserName)}",
            $"email={Normalize(request.Email)}",
            $"dob={FormatDate(request.DateOfBirth)}",
            $"courseTitle={Normalize(request.CourseTitle)}",
            $"instructor={Normalize(request.InstructorName)}",
            $"priceFrom={request.PriceFrom?.ToString() ?? "null"}",
            $"priceTo={request.PriceTo?.ToString() ?? "null"}",
            $"enrolledFrom={FormatDate(request.EnrolledFrom)}",
            $"enrolledTo={FormatDate(request.EnrolledTo)}",
            $"orderBy={Normalize(request.OrderByData)}",
            $"page={request.Page}",
            $"size={request.PageSize}"
        );
    }

    private static string Normalize(string? value)
        => string.IsNullOrWhiteSpace(value)
            ? "null"
            : value.Trim().ToLowerInvariant();

    private static string FormatDate(DateTime? date)
        => date.HasValue
            ? date.Value.ToString("yyyyMMdd")
            : "null";
    private static string FormatDate(DateOnly? date)
        => date.HasValue
            ? date.Value.ToDateTime(TimeOnly.MinValue).ToString("yyyyMMdd")
            : "null";
}
