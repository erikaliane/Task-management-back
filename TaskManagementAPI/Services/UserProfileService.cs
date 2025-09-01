using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data.Models;
using TaskManagementAPI.DTO.Response;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly TaskManagementDbContext _context;

        public UserProfileService(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProfileDto>> GetEmployeeProfilesAsync()
        {
            var result = await _context.Users
                .Where(u => u.Role == "Employee")
                .Join(_context.UserProfiles,
                      u => u.UserId,
                      p => p.UserId,
                      (u, p) => new UserProfileDto
                      {
                          UserId = u.UserId,
                          FirstName = p.FirstName,
                          LastName = p.LastName
                      })
                .ToListAsync();

            return result;
        }
    }
}