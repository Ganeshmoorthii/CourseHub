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
    public class UserProfileService : IUserProfileService
    {
        private readonly ILogger<UserProfileService> _logger;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        public UserProfileService(ILogger<UserProfileService> logger, IUserProfileRepository userProfileRepository,IMapper mapper)
        {
            _logger = logger;
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }

        public async Task CreateUserProfileAsync(CreateUserProfileDTO dto)
        {
            _logger.LogInformation("CreateUserProfileAsync will be in the Service Layer");
            var newUserProfile = _mapper.Map<UserProfile>(dto);
            await _userProfileRepository.CreateUserProfile(newUserProfile);
        }
    }
}
