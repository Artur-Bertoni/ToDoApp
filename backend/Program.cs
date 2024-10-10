using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using System;

namespace ToDoList {
    public class Program {
        public static void Main(string[] args) {
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) {

                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
                catch (DbUpdateException ex) {
                   var logger = services.GetRequiredService<ILogger<Program>>();
                   logger.LogError(ex, "Erro ao aplicar migrações.");
                }
                catch (InvalidOperationException ex) {
                   var logger = services.GetRequiredService<ILogger<Program>>();
                   logger.LogError(ex, "Erro de injeção de dependência ou serviço não registrado.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
    