using CourseHub.API.Contracts;
using CourseHub.Application.Contracts;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using CourseHub.Infrastructure.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class EnrrollmentController : ControllerBase
    {
        private readonly ILogger<EnrrollmentController> _logger;
        private readonly IEnrollmentService _enrollment;
        public EnrrollmentController(ILogger<EnrrollmentController> logger, IEnrollmentService enrollment)
        {
            _logger = logger;
            _enrollment = enrollment;
        }

        /// <summary>
        /// Create a new Course Enrollment
        /// </summary>
        /// <param name="dto">Course Enrollment creation DTO</param>
        /// <returns>Created course enrollment</returns>
        /// <response code="201">Course Enrollment created successfully</response>
        /// <response code="400">Validation failed (custom exception handled globally)</response>
        /// <response code="404">Resource not found (custom exception handled globally)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentRequestDTO dto)
        {
            _logger.LogInformation("CreateEnrollment endpoint called.");
            await _enrollment.CreateEnrollmentAsync(dto);
            return Created(string.Empty, ApiResponse<object>.Created(null, "Course Enrollment created successfully")); 
        }
    }
}
