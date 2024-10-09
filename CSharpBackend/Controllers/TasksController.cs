
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<ToDoTask> tasks = new List<ToDoTask>
        {
            new ToDoTask { Id = 1, Name = "Sample Task", Description = "This is a sample task", Completed = false }
        };

        [HttpGet]
        public IActionResult GetTasks() => Ok(tasks);

        [HttpPost]
        public IActionResult CreateTask([FromBody] ToDoTask newTask)
        {
            newTask.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(newTask);
            return Ok(newTask);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            tasks.Remove(task);
            return Ok();
        }

        [HttpPut("{id}/complete")]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            task.Completed = true;
            return Ok();
        }
    }
}
    