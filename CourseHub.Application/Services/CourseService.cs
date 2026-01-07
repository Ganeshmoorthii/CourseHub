using AutoMapper;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using CourseHub.Domain.Entities;
using CourseHub.Infrastructure.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ILogger<CourseService> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseService(ILogger<CourseService> logger, ICourseRepository courseRepository, IMapper mapper)
        {
            _logger = logger;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task CreateCourseAsync(CreateCourseRequestDTO courseRequestDTO)
        {
            _logger.LogInformation("CreateCourseAsync method called in CourseService.");
            var courseEntity = _mapper.Map<Course>(courseRequestDTO);
            await _courseRepository.AddCourseAsync(courseEntity);
            _logger.LogInformation("Course created successfully in the repository.");
        }
    }
}
