using AutoMapper;
using CourseHub.Application.DTOs.Request;
using CourseHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Application.MappingProfiles
{
    public class EnrollmentMappingProfile : Profile
    {
        public EnrollmentMappingProfile()
        {
            CreateMap<Enrollment, CreateEnrollmentRequestDTO>().ReverseMap();
        }
    }
}
