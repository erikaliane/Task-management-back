using System;

namespace TaskManagementAPI.DTO.Response
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string? AssignedFirstName { get; set; }
        public string? AssignedLastName { get; set; }
    }
}