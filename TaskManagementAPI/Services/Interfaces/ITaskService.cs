using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementAPI.DTO.Request;
using TaskManagementAPI.DTO.Response;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllAsync();
        Task<TaskDto?> GetByIdAsync(int id);
        Task<TaskDto> CreateAsync(TaskCreateDto dto);
        Task<bool> UpdateAsync(int id, TaskUpdateDto dto);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> AssignTaskAsync(int taskId, int userId);
        Task<IEnumerable<TaskDto>> GetTasksByUserAsync(int userId, bool isDeleted);
        Task<bool> UpdateStatusAsync(int taskId, string status);
    }
}