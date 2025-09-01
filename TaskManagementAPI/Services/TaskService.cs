using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data.Models;
using TaskManagementAPI.DTO.Request;
using TaskManagementAPI.DTO.Response;
using TaskManagementAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagementDbContext _context;

        public TaskService(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync()
        {
            
            var query = from task in _context.Tasks
                        where !task.IsDeleted
                        join profile in _context.UserProfiles
                            on task.AssignedTo equals profile.UserId into profileGroup
                        from profile in profileGroup.DefaultIfEmpty()
                        select new TaskDto
                        {
                            TaskId = task.TaskId,
                            Title = task.Title,
                            Description = task.Description,
                            Status = task.Status,
                            Priority = task.Priority,
                            DueDate = task.DueDate,
                            AssignedTo = task.AssignedTo,
                            CreatedAt = task.CreatedAt,
                            UpdatedAt = task.UpdatedAt,
                            AssignedFirstName = profile != null ? profile.FirstName : null,
                            AssignedLastName = profile != null ? profile.LastName : null
                        };

            return await query.ToListAsync();
        }

        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            var task = await _context.Tasks
                .Where(x => x.TaskId == id && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (task == null) return null;

            var profile = task.AssignedTo.HasValue
                ? await _context.UserProfiles
                    .Where(p => p.UserId == task.AssignedTo.Value)
                    .FirstOrDefaultAsync()
                : null;

            return new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                AssignedTo = task.AssignedTo,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                AssignedFirstName = profile?.FirstName,
                AssignedLastName = profile?.LastName
            };
        }

        public async Task<TaskDto> CreateAsync(TaskCreateDto dto)
        {
            var now = DateTime.UtcNow;
            var task = new Data.Models.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                AssignedTo = dto.AssignedTo,
                CreatedAt = now,
                UpdatedAt = now,
                IsDeleted = false
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var profile = task.AssignedTo.HasValue
                ? await _context.UserProfiles
                    .Where(p => p.UserId == task.AssignedTo.Value)
                    .FirstOrDefaultAsync()
                : null;

            return new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                AssignedTo = task.AssignedTo,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                AssignedFirstName = profile?.FirstName,
                AssignedLastName = profile?.LastName
            };
        }

        public async Task<bool> UpdateAsync(int id, TaskUpdateDto dto)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == id && !x.IsDeleted);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.AssignedTo = dto.AssignedTo;
            task.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == id && !x.IsDeleted);
            if (task == null) return false;
            task.IsDeleted = true;
            task.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> AssignTaskAsync(int taskId, int userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId && !x.IsDeleted);
            if (task == null) return false;
            task.AssignedTo = userId;
            task.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int taskId, string status)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId && !x.IsDeleted);
            if (task == null) return false;

            task.Status = status;
            task.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByUserAsync(int userId, bool isDeleted)
        {
         
            var tasks = await _context.Tasks
                .FromSqlInterpolated($"EXEC GetTasksByUser @UserId = {userId}, @IsDeleted = {(isDeleted ? 1 : 0)}")
                .AsNoTracking() 
                .ToListAsync();

        
            var result = tasks.Select(task => new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                AssignedTo = task.AssignedTo,
                Status = task.Status,
                Priority = task.Priority,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            });

            return result;
        }
    }
}