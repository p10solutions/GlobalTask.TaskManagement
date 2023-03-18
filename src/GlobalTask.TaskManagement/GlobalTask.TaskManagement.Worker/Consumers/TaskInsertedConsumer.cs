using System.Diagnostics;
using GlobalTask.TaskManagement.Application.Models.Events.Tasks;
using GlobalTask.TaskManagement.Application.Models.Events.Tasks.Maps;
using MassTransit;
using MassTransit.Metadata;
using MediatR;

namespace GlobalTask.TaskManagement.Worker.Consumers;

public class TaskInsertedConsumer : IConsumer<TaskInsertedEvent>
{
    readonly ILogger<TaskInsertedConsumer> _logger;
    readonly IMediator _mediator;

    public TaskInsertedConsumer(ILogger<TaskInsertedConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<TaskInsertedEvent> context)
    {
        var timer = Stopwatch.StartNew();

        try
        {
            var message = context.Message;

            if (message == null)
                return;

            _logger.LogInformation("A new task has been received");
            await _mediator.Send(TaskInsertedMapper.MapTo(message));

            await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<TaskInsertedEvent>.ShortName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error:", ex.Message);
            await context.NotifyFaulted(timer.Elapsed, TypeMetadataCache<TaskInsertedEvent>.ShortName, ex);
        }
    }
}

public class QueueClientConsumerDefinition : ConsumerDefinition<TaskInsertedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TaskInsertedConsumer> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}