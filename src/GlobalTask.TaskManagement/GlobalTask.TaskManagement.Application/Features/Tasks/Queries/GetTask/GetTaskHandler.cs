using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTask
{
    public class GetTaskHandler : IRequestHandler<GetTaskQuery, IEnumerable<GetTaskResponse>>
    {
        readonly ITaskRepository _taskRepository;
        readonly ILogger<GetTaskHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public GetTaskHandler(ITaskRepository taskRepository, ILogger<GetTaskHandler> logger, INotificationsHandler notificationsHandler)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<IEnumerable<GetTaskResponse>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _taskRepository.GetAsync();
                var response = GetTaskMapper.MapFrom(tasks);

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the task: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the task", ENotificationType.InternalError)
                        .ReturnDefault<IEnumerable<GetTaskResponse>>();
            }
        }
    }
}
