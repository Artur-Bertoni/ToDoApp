
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("/api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<ToDoTask> tasks = new List<ToDoTask>
        {
            new ToDoTask { Id = 1, Name = "Sample Task", Description = "This is a sample task", Completed = false }
        };

        [HttpGet]
        public IActionResult GetTasks() {
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] ToDoTask newTask)
        {
            newTask.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(newTask);

            Console.WriteLine($"Task added - Name: {newTask.Name}, Description: {newTask.Description}, Deadline: {newTask.Deadline}");

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

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] ToDoTask updatedTask)
        {
            if (id != updatedTask.Id)
            {
                return BadRequest();
            }

            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            task.Name = updatedTask.Name;
            task.Description = updatedTask.Description;
            task.Deadline = updatedTask.Deadline;

            return Ok(task);
        }
    }
}
    