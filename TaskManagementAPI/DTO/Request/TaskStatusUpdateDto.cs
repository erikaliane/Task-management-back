using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTO.Request
{
    public class TaskStatusUpdateDto
    {
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede tener más de 50 caracteres.")]
        public string Status { get; set; } = null!;
    }
}