using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly IUserProfileService _userProfileService;
        public UserProfileController(ILogger<UserProfileController> logger, IUserProfileService userProfileService)
        {
            _logger = logger;
            _userProfileService = userProfileService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserProfile(CreateUserProfileDTO dto)
        {
            _logger.LogInformation("CreateUserProfile endpoint called.");
            await _userProfileService.CreateUserProfileAsync(dto);
            return Ok("User profile created successfully.");
        }
    }
}
