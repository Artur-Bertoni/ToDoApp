using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;
using ToDoList.Data;

namespace ToDoList.Services {
    public class TaskService {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<ToDoTask> GetAllTasks() {
            return _context.ToDoTasks.ToList();
        }

        public ToDoTask GetTaskById(int id) {
            return _context.ToDoTasks.Find(id);
        }

        public ToDoTask AddTask(ToDoTask task) {
            _context.ToDoTasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public bool UpdateTask(int id, ToDoTask task) {
            if (id != task.Id) {
                return false;
            }

            var existingTask = _context.ToDoTasks.SingleOrDefault(t => t.Id == id);
            if (existingTask == null) {
                return false;
            }

            _context.Entry(existingTask).State = EntityState.Detached;

            _context.ToDoTasks.Attach(task);
            _context.Entry(task).State = EntityState.Modified;

            _context.SaveChanges();
            return true;
        }

        public bool ToggleTaskCompletion(int id) {
            var task = GetTaskById(id);
            if (task == null) {
                return false;
            }

            task.Completed = !task.Completed;

            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteTask(int id) {
            var task = GetTaskById(id);
            if (task == null) {
                return false;
            }

            _context.ToDoTasks.Remove(task);
            _context.SaveChanges();
            return true;
        }
    }
}
