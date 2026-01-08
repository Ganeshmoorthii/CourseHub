using CourseHub.API.Contracts;
using CourseHub.Application.Contracts;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.DTOs.Response;
using CourseHub.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="dto">User creation request</param>
        /// <returns>201 if user created successfully</returns>
        /// <response code="201">User created successfully</response>
        /// <response code="400">Validation failed (handled globally)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO dto)
        {
            _logger.LogInformation("CreateUser endpoint called.");

            await _userService.CreateUserAsync(dto);

            return Created(
                string.Empty,
                ApiResponse<object>.Created(null, "User created successfully")
            );
        }

        /// <summary>
        /// Search users with filters and pagination
        /// </summary>
        /// <param name="request">Search filters and pagination parameters</param>
        /// <returns>Paged list of users</returns>
        /// <response code="200">Users retrieved successfully</response>
        /// <response code="400">Validation failed (handled globally)</response>
        [HttpPost("search")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserSearchDTO>>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchUsers([FromBody] UserSearchRequestDTO request)
        {
            _logger.LogInformation("SearchUsers endpoint called.");

            var result = await _userService.SearchUsersAsync(request);

            return Ok(
                ApiResponse<PagedResult<UserSearchDTO>>.Ok(
                    result,
                    "Users retrieved successfully"
                )
            );
        }
    }
}
