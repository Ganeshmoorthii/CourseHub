using AutoMapper;
using CourseHub.Application.DTOs.Request;
using CourseHub.Application.IServices;
using CourseHub.Infrastructure.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository,IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(CreateUserRequestDTO dto)
        {
            _logger.LogInformation("CreateUserAsync called in UserService.");
            var newUser = _mapper.Map<Domain.Entities.User>(dto);
            await _userRepository.AddUserAsync(newUser);
        }
    }
}
