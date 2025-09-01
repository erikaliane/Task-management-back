using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTO.Request;
using TaskManagementAPI.DTO.Response;
using TaskManagementAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")] 
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        // Listar todas las tareas (sin eliminadas)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // Obtener tarea por ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TaskDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Crear tarea
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TaskDto>> Create([FromBody] TaskCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve los errores de validación
            }

            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.TaskId }, result);
        }

        // Editar tarea
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(int id, [FromBody] TaskUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve los errores de validación
            }
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // Soft delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var success = await _service.SoftDeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // Asignar tarea
        [HttpPatch("{id}/assign/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AssignTask(int id, int userId)
        {
            var success = await _service.AssignTaskAsync(id, userId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("employee/user/{userId}")]
        [Authorize(Roles = "Employee")] 
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByUserForEmployee(int userId, [FromQuery] bool isDeleted = false)
        {
            var result = await _service.GetTasksByUserAsync(userId, isDeleted);
            return Ok(result);
        }

     
        [HttpPatch("employee/task/{id}/status")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> UpdateTaskStatus(int id, [FromBody] TaskStatusUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var success = await _service.UpdateStatusAsync(id, dto.Status);
            if (!success) return NotFound(); 

            return NoContent(); 
        }
    }
}