public class TaskDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskStatus? Status { get; set; }  // 0 - Pending, 1 - Completed, 3 - Overdue
}
