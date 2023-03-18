using GlobalTask.TaskManagement.Infra.IoC;
using GlobalTask.TaskManagement.Worker.Consumers;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //var appSettings = new AppSettings();
        //context.Configuration.Bind(appSettings);
        //collection.AddOpenTelemetry(appSettings);
        //collection.AddHttpContextAccessor();
        IConfiguration configuration = context.Configuration;
        services.AddProviders(configuration);
        services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();
            x.AddConsumer<TaskInsertedConsumer>(typeof(QueueClientConsumerDefinition));
            x.AddConsumer<TaskUpdatedConsumer>(typeof(QueueClientUpdatedConsumerDefinition));

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
                cfg.UseDelayedMessageScheduler();
                cfg.ServiceInstance(instance =>
                {
                    instance.ConfigureJobServiceEndpoints();
                    instance.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                });
            });
        });
    })
    .Build();

await host.RunAsync();
