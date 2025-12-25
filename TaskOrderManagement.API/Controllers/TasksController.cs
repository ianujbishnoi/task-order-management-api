using Microsoft.AspNetCore.Mvc;
using TaskOrderManagement.Application.DTOs;
using TaskOrderManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace TaskOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
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

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagedRequestDto request)
        {
            var result = await _taskService.GetPagedTasksAsync(request);
            return Ok(result);
        }

        [Authorize(Policy = "CanCreateTaskPolicy")]
        [HttpPost]
        public async Task<IActionResult> CreateTask()
        {
            return Ok("Task created (policy-based access)");
        }

        [Authorize(Policy = "ITDepartmentOnly")]
        [HttpGet("it-only")]
        public IActionResult ITOnlyEndpoint()
        {
            return Ok("Accessible only to IT department users");
        }

    }
}
