using CourseHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Infrastructure.IRepository
{
    public interface IUserProfileRepository
    {
        public Task CreateUserProfile(UserProfile userProfile);
    }
}
