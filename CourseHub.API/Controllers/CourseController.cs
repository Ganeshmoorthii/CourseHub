using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _courseService;
        public CourseController(ILogger<CourseController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="dto">Course creation DTO</param>
        /// <returns>Created course</returns>
        /// <response code="200">Course created successfully</response>
        /// <response code="400">Validation failed (custom exception handled globally)</response>
        /// <response code="404">Resource not found (custom exception handled globally)</response>
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequestDTO courseRequestDTO)
        {
            _logger.LogInformation("CreateCourse endpoint called.");
            await _courseService.CreateCourseAsync(courseRequestDTO);
            return Ok("Course created successfully.");
        }
    }
}
