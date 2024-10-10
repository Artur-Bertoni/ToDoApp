using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Adicione a string de conexão com os parâmetros do SQL Server
                optionsBuilder.UseSqlServer("Server=your_server_name;Database=your_database_name;User Id=your_username;Password=your_password;");
            }
        }

        // Método para buscar todas as tasks
        public IEnumerable<ToDoTask> GetAllTasks()
        {
            return ToDoTasks.ToList();
        }

        // Método para buscar uma task pelo ID
        public ToDoTask GetTaskById(int id)
        {
            return ToDoTasks.Find(id);
        }

        // Método para adicionar uma nova task
        public void AddTask(ToDoTask task)
        {
            ToDoTasks.Add(task);
            SaveChanges();
        }

        // Método para atualizar uma task existente
        public void UpdateTask(ToDoTask task)
        {
            ToDoTasks.Update(task);
            SaveChanges();
        }

        // Método para deletar uma task pelo ID
        public void DeleteTask(int id)
        {
            var task = ToDoTasks.Find(id);
            if (task != null)
            {
                ToDoTasks.Remove(task);
                SaveChanges();
            }
        }
    }
}
