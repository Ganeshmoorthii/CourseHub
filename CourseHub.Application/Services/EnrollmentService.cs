using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.IRepository;
using Microsoft.Extensions.Logging;

namespace CourseHub.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ILogger<EnrollmentService> _logger;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(
            ILogger<EnrollmentService> logger,
            IEnrollmentRepository enrollmentRepository,
            IUserRepository userRepository,
            ICourseRepository courseRepository)
        {
            _logger = logger;
            _enrollmentRepository = enrollmentRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        public async Task CreateEnrollmentAsync(CreateEnrollmentRequestDTO dto)
        {
            _logger.LogInformation("Enrollment started");

            if (!await _userRepository.ExistsAsync(dto.UserId))
                throw new Exception("User not found");

            if (!await _courseRepository.ExistsAsync(dto.CourseId))
                throw new Exception("Course not found");

            if (await _enrollmentRepository.ExistsAsync(dto.UserId, dto.CourseId))
                throw new Exception("User already enrolled in this course");

            var enrollment = new Enrollment
            {
                UserId = dto.UserId,
                CourseId = dto.CourseId,
                EnrolledAt = DateTime.UtcNow,
                Status = "Active"
            };

            await _enrollmentRepository.AddAsync(enrollment);

            _logger.LogInformation("Enrollment completed successfully");
        }
    }
}
