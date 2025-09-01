using TaskManagementAPI.DTO.Response;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<IEnumerable<UserProfileDto>> GetEmployeeProfilesAsync();
    }
}