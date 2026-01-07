using AutoMapper;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.IRepository;
using Microsoft.Extensions.Logging;

namespace CourseHub.Application.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly ILogger<InstructorService> _logger;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;
        public InstructorService(ILogger<InstructorService> logger, IInstructorRepository instructorRepository,IMapper mapper)
        {
            _logger = logger;
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }
        public async Task CreateInstructorAsync(CreateInstructorRequestDTO dto)
        {
            _logger.LogInformation("CreateInstructorAsync will be in the Instructor Service");
            var newInstructor = _mapper.Map<Instructor>(dto);
            await _instructorRepository.CreateInstructor(newInstructor);
        }
    }
}
