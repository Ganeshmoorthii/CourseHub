using CourseHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseHub.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User newUser);
        public Task<bool> ExistsAsync(Guid userId);
    }
}
