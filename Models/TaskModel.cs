using System.ComponentModel.DataAnnotations;

public class TaskModel
{
    [Key]
    public int TaskID { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }  // Nullable for optional dates
    public TaskStatus? Status { get; set; }  // Enum for status (Pending, Completed, Overdue)
    
}

public enum TaskStatus
{
    Pending = 0,
    Completed = 1,
    Overdue = 3
}
