using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService) {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoTask>> GetTasks() {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoTask> GetTask(int id) {
            var task = _taskService.GetTaskById(id);
            if (task == null) {
                return NotFound("Task not found.");
            }

            return Ok(task);
        }

        [HttpPost]
        public ActionResult<ToDoTask> PostTask(ToDoTask task) {
            var createdTask = _taskService.AddTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, ToDoTask task) {
            if (id != task.Id) {
                return BadRequest("Task ID mismatch.");
            }

            var success = _taskService.UpdateTask(id, task);
            if (!success) {
                return NotFound("Task not found or failed to update.");
            }

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult ToggleCompletion(int id) {
            var task = _taskService.GetTaskById(id);
            if (task == null) {
                return NotFound();
            }

            var success = _taskService.ToggleTaskCompletion(id);
            if (!success) {
                return BadRequest("Failed to toggle task completion.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id) {
            var success = _taskService.DeleteTask(id);
            if (!success) {
                return NotFound("Task not found or failed to delete.");
            }

            return NoContent();
        }
    }
}
