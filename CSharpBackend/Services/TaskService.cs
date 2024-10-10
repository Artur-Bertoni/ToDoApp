using System.Collections.Generic;
using System.Linq;
using CSharpBackend.Models;

namespace CSharpBackend.Services
{
    public class TaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ToDoTask> GetAllTasks()
        {
            return _context.ToDoTasks.ToList();
        }

        public ToDoTask GetTaskById(int id)
        {
            return _context.ToDoTasks.Find(id);
        }

        public void AddTask(ToDoTask task)
        {
            _context.ToDoTasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(ToDoTask task)
        {
            _context.ToDoTasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.ToDoTasks.Find(id);
            if (task != null)
            {
                _context.ToDoTasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
