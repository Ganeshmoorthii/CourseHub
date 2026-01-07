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
        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentRequestDTO dto)
        {
            _logger.LogInformation("CreateEnrollment endpoint called.");
            await _enrollment.CreateEnrollmentAsync(dto);
            return Ok("Enrollment created successfully.");
        }
    }
}
