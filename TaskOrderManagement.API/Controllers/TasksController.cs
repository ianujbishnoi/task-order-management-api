using Microsoft.AspNetCore.Mvc;
using TaskOrderManagement.Application.Interfaces;

namespace TaskOrderManagement.API.Controllers
{
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTasks(int userId)
        {
            var tasks = await _taskService.GetTasksByUserAsync(userId);
            return Ok(tasks);
        }
    }
}
