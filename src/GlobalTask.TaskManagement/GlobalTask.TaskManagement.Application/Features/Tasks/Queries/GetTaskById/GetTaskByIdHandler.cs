using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, GetTaskByIdResponse>
    {
        readonly ITaskRepository _taskRepository;
        readonly ILogger<GetTaskByIdHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public GetTaskByIdHandler(ITaskRepository taskRepository, ILogger<GetTaskByIdHandler> logger, INotificationsHandler notificationsHandler)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<GetTaskByIdResponse> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskRepository.GetAsync(request.Id);

                var response = GetTaskByIdMapper.MapFrom(task);

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the task: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the task", ENotificationType.InternalError)
                        .ReturnDefault<GetTaskByIdResponse>();
            }
        }
    }
}
