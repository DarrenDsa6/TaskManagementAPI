using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PATCH: api/Tasks/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO task)
        {
            if (task == null)
            {
                return BadRequest("Task data is required.");
            }

            if (!Enum.IsDefined(typeof(TaskStatus), task.Status))
            {
                return BadRequest("Invalid status value.");
            }

            // Find the existing task by ID
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            // Only update the fields if they are provided
            if (!string.IsNullOrWhiteSpace(task.Title))
            {
                existingTask.Title = task.Title;
            }

            if (!string.IsNullOrWhiteSpace(task.Description))
            {
                existingTask.Description = task.Description;
            }

            // Update DueDate if it's provided (nullable)
            if (task.DueDate.HasValue)
            {
                existingTask.DueDate = task.DueDate.Value;
            }

            // Ensure status is valid before updating
            if (Enum.IsDefined(typeof(TaskStatus), task.Status))
            {
                existingTask.Status = (TaskStatus)task.Status;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingTask); // Success response
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            if (task == null)
            {
                return BadRequest("Task data cannot be null.");
            }

            // Check for null or whitespace in string fields
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Title cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(task.Description))
            {
                return BadRequest("Description cannot be empty or whitespace.");
            }

            // Check if DueDate is provided (if it's required)
            if (!task.DueDate.HasValue)
            {
                return BadRequest("Due date is required.");
            }

            // Ensure status is valid before creating the task
            if (!Enum.IsDefined(typeof(TaskStatus), task.Status))
            {
                return BadRequest("Invalid status value.");
            }

            // Add task to the database
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskID }, task); // Return created task
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            // Find the task by ID
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                // Return NotFound if the task does not exist
                return NotFound();
            }

            // Remove the task from the database
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            // Return NoContent as a successful deletion response
            return NoContent();
        }

    }
}
