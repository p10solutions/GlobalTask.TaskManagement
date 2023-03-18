using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        readonly ITaskRepository _taskRepository;
        readonly ILogger<CreateTaskHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public CreateTaskHandler(ITaskRepository taskRepository, ILogger<CreateTaskHandler> logger, INotificationsHandler notificationsHandler)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = CreateTaskCommandMapper.MapTo(request);

            try
            {
                await _taskRepository.AddAsync(task);

                return task.Id;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to create a new task: {exception}", ex.Message);
                return _notificationsHandler
                        .AddNotification("An error occurred when trying to create a new task", ENotificationType.InternalError)
                        .ReturnDefault<Guid>();
            }
        }
    }
}
