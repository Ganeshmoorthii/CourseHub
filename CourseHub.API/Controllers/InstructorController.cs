using CourseHub.API.Contracts;
using CourseHub.Application.Contracts;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly ILogger<InstructorController> _logger;
        public InstructorController(IInstructorService instructorService, ILogger<InstructorController> logger)
        {
            _instructorService = instructorService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new instructor
        /// </summary>
        /// <param name="dto">Instructor creation DTO</param>
        /// <returns>Created Instructor</returns>
        /// <response code="201">Instructor created successfully</response>
        /// <response code="400">Validation failed (custom exception handled globally)</response>
        /// <response code="404">Resource not found (custom exception handled globally)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInstructor(CreateInstructorRequestDTO dto)
        {
            _logger.LogInformation("CreateInstructor endpoint called.");
            await _instructorService.CreateInstructorAsync(dto);
            return Created(string.Empty, ApiResponse<object>.Created(null,"Instructor created successfully"));
        }
    }
}
