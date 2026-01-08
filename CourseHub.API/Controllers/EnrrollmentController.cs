using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using CourseHub.Infrastructure.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// <response code="200">Course Enrollment created successfully</response>
        /// <response code="400">Validation failed (custom exception handled globally)</response>
        /// <response code="404">Resource not found (custom exception handled globally)</response>
        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentRequestDTO dto)
        {
            _logger.LogInformation("CreateEnrollment endpoint called.");
            await _enrollment.CreateEnrollmentAsync(dto);
            return Ok("Enrollment created successfully.");
        }
    }
}
