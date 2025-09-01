using System;
using System.Collections.Generic;

namespace TaskManagementAPI.Data.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public int? AssignedTo { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User? AssignedToNavigation { get; set; }
}
