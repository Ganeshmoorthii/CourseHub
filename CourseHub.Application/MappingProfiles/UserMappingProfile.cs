using AutoMapper;
using CourseHub.Application.DTOs.Request;
using CourseHub.Domain.Entities;

namespace CourseHub.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserRequestDTO, User>()
            .ForMember(d => d.PasswordHash, o => o.Ignore());
            CreateMap<CreateUserProfileDTO, UserProfile>();
        }
    }
}
