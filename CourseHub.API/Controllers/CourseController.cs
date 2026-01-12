using CourseHub.API.Contracts;
using CourseHub.Application.Contracts;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.DTOs.Response;
using CourseHub.Application.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _courseService;
        public CourseController(ILogger<CourseController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        /// <Summary>
        /// Get All Courses by using Pagination
        /// </Summary>
        /// <param name="page">Page Starting Number</param> 
        /// <param name="pageSize">Page Total Size</param>
        /// <returns>Shows Number of Courses based on the page size</returns>
        /// <response code="200">Get the Courses</response>
        /// /// <response code="400">Validation failed (custom exception handled globally)</response>
        /// <response code="404">Resource not found (custom exception handled globally)</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CourseInfoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCourses(int page = 1, int pageSize = 10)
        {
            var courses = await _courseService.GetCoursesAsync(page, pageSize);

            return Ok(
                ApiResponse<IEnumerable<CourseInfoDTO>>.Ok(
                    courses,
                    "Courses fetched successfully"
                )
            );
        }



        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="dto">Course creation DTO</param>
        /// <returns>Created course</returns>
        /// <response code="201">Course created successfully</response>
        /// <response code="400">Validation failed (custom exception handled globally)</response>
        /// <response code="404">Resource not found (custom exception handled globally)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequestDTO courseRequestDTO)
        {
            _logger.LogInformation("CreateCourse endpoint called.");
            await _courseService.CreateCourseAsync(courseRequestDTO);
            return Created(string.Empty, ApiResponse<object>.Created(null, "Course created successfully"));
        }

    }
}
