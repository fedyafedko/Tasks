using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.BLL.Services.Interfaces;
using Tasks.Common.DTOs;
using Tasks.Common.DTOs.Task;
using Tasks.Common.Requests;
using Tasks_BE.Extensions;

namespace Tasks_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Add task.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> This endpoint returns a task.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTask(CreateTaskDTO dto)
        {
            var userId = HttpContext.GetUserId();

            var result = await _taskService.AddAsync(userId, dto);

            return Ok(result);
        }

        /// <summary>
        /// Update task.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns> This endpoint returns a task.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TaskDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDTO dto)
        {
            var userId = HttpContext.GetUserId();

            var result = await _taskService.UpdateAsync(dto, id, userId);

            return Ok(result);
        }

        /// <summary>
        /// Delete task.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> This endpoint returns a status code.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userId = HttpContext.GetUserId();

            var result = await _taskService.DeleteAsync(userId, id);

            return result ? NoContent() : NotFound();
        }

        /// <summary>
        /// Get task by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> This endpoint returns a task.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var result = await _taskService.GetAsync(id);

            return Ok(result);
        }

        /// <summary>
        /// Get tasks.
        /// </summary>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a tasks with pagination.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageList<TaskDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTasks([FromQuery] GetTasksRequest request)
        {
            var result = await _taskService.GetAllAsync(request);

            return Ok(result);
        }
    }
}
