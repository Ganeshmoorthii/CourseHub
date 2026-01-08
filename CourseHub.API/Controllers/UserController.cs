using CourseHub.Application.DTOs.Request;
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
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="dto">User creation DTO</param>
        /// <returns>Returns 200 if user created successfully</returns>
        /// <response code="200">User created successfully</response>
        /// <response code="400">Validation failed (custom exception handled globally)</response>
        /// <response code="404">Resource not found (custom exception handled globally)</response>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO dto)
        {
            _logger.LogInformation("CreateUser endpoint called.");
            await _userService.CreateUserAsync(dto);
            return Ok("User created successfully.");
        }
    }
}
