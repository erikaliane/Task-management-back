using System;
using System.ComponentModel.DataAnnotations;
namespace TaskManagementAPI.DTO.Request
{
    public class TaskUpdateDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        [StringLength(50, ErrorMessage = "La prioridad no puede tener más de 50 caracteres.")]
        public string Priority { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "La fecha de vencimiento debe ser válida.")]
        public DateTime DueDate { get; set; }

        public int? AssignedTo { get; set; }
    }
}