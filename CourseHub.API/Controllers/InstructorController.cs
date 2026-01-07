using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly ILogger<InstructorController> _logger;
        public InstructorController(IInstructorService instructorService, ILogger<InstructorController> logger)
        {
            _instructorService = instructorService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateInstructor(CreateInstructorRequestDTO dto)
        {
            _logger.LogInformation("CreateInstructor endpoint called.");
            await _instructorService.CreateInstructorAsync(dto);
            return Ok("Instructor created successfully.");
        }
    }
}
