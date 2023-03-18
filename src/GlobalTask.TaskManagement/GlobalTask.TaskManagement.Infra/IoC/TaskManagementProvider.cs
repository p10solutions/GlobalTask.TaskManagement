using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask;
using GlobalTask.TaskManagement.Application.Features.Tasks.Commands.UpdateTask;
using GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTask;
using GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTaskById;
using GlobalTask.TaskManagement.Infra.Data;
using GlobalTask.TaskManagement.Infra.Validation;
using GlobalTaskTaskManagement.Infra.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalTask.TaskManagement.Infra.IoC
{
    public static class TaskManagementProvider
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("taskManagement");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTaskByIdHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTaskHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateTaskHandler).Assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastValidator<,>));
            services.AddScoped<INotificationsHandler, NotificationHandler>();
            services.AddDbContextPool<TaskManagementContext>(opt => opt.UseSqlServer(connectionString));
            services.AddTransient<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
