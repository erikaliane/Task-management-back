using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTO.Response;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/profile")]
    [Authorize(Roles = "Admin")] 
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _service;

        public UserProfileController(IUserProfileService service)
        {
            _service = service;
        }

        // GET: api/userprofile/employee-profiles
        [HttpGet("employee-profiles")]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetEmployeeProfiles()
        {
            var result = await _service.GetEmployeeProfilesAsync();
            return Ok(result);
        }
    }
}