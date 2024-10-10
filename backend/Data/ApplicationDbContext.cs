using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public IEnumerable<ToDoTask> GetAllTasks() {
            return ToDoTasks.ToList();
        }

        public ToDoTask GetTaskById(int id) {
            return ToDoTasks.Find(id);
        }

        public void AddTask(ToDoTask task) {
            ToDoTasks.Add(task);
            SaveChanges();
        }

        public void UpdateTask(ToDoTask task) {
            ToDoTasks.Update(task);
            SaveChanges();
        }

        public void DeleteTask(int id) {
            var task = ToDoTasks.Find(id);
            if (task != null) {
                ToDoTasks.Remove(task);
                SaveChanges();
            }
        }
    }
}
