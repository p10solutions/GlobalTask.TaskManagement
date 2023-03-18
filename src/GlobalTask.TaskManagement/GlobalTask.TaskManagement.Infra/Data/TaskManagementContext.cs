
using GlobalTask.TaskManagement.Application.Entities;
using GlobalTask.TaskManagement.Infra.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace GlobalTask.TaskManagement.Infra.Data
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
        : base(options)
        {

        }

        public DbSet<TaskItem> TaskItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskItemMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
