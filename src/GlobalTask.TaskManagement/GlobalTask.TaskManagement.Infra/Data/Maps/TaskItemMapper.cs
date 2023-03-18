using Microsoft.EntityFrameworkCore;
using GlobalTask.TaskManagement.Application.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalTask.TaskManagement.Infra.Data.Maps
{
    public class TaskItemMapper : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description).HasColumnType("varchar(200)");
        }
    }
}
