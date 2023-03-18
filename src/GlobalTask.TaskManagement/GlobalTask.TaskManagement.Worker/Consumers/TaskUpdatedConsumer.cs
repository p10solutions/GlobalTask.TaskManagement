using GlobalTask.TaskManagement.Application.Models.Events.Tasks;
using GlobalTask.TaskManagement.Application.Models.Events.Tasks.Maps;
using MassTransit;
using MassTransit.Metadata;
using MediatR;
using System.Diagnostics;

namespace GlobalTask.TaskManagement.Worker.Consumers;

public class TaskUpdatedConsumer : IConsumer<TaskUpdatedEvent>
{
    private readonly ILogger<TaskUpdatedConsumer> _logger;
    readonly IMediator _mediator;

    public TaskUpdatedConsumer(ILogger<TaskUpdatedConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<TaskUpdatedEvent> context)
    {
        var timer = Stopwatch.StartNew();

        try
        {
            var message = context.Message;

            if (message == null)
                return;

            _logger.LogInformation("A new task has been changed Id:{TaskId}", message.Id);
            await _mediator.Send(TaskUpdatedMapper.MapTo(message));

            await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<TaskInsertedEvent>.ShortName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error:", ex.Message);
            await context.NotifyFaulted(timer.Elapsed, TypeMetadataCache<TaskInsertedEvent>.ShortName, ex);
        }
    }
}

public class QueueClientUpdatedConsumerDefinition : ConsumerDefinition<TaskUpdatedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TaskUpdatedConsumer> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}