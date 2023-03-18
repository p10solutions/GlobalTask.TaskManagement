using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Guid>
    {
        readonly ITaskRepository _taskRepository;
        readonly ILogger<UpdateTaskHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public UpdateTaskHandler(ITaskRepository taskRepository, ILogger<UpdateTaskHandler> logger, INotificationsHandler notificationsHandler)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<Guid> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = UpdateTaskCommandMapper.MapTo(request);

            try
            {
                await _taskRepository.UpdateAsync(task);

                return task.Id;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to Update the task: {exception}", ex.Message);
                return _notificationsHandler
                        .AddNotification("An error occurred when trying to Update the task", ENotificationType.InternalError)
                        .ReturnDefault<Guid>();
            }
        }
    }
}
