using Domain.Shared.Events.DomainEvents;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Users.Infrastructure.Processing.MassTransitExtensions;

public static class MessageQueueWithConsumers
{
    public static void RegisterMessageQueueWithConsumers(this IServiceCollection services, RabbitMqConfig rabbitMqConfig)
    {
        var domainEventType = typeof(IDomainEventHandler<>);
        var types = domainEventType.FindAllInplementationTypesByType();
        
        services.AddMassTransit(config =>
        {
            config.AddHealthChecks();

            config.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = (check) => check.Tags.Contains("ready");
            });
            
            config.AddConsumers(types.ToArray());

            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqConfig.Host, rabbitMqConfig.Port, rabbitMqConfig.VirtualHost, rabbitConfig =>
                {
                    rabbitConfig.Username(rabbitMqConfig.Username);
                    rabbitConfig.Password(rabbitMqConfig.Password);
                });
                
                cfg.UseRawJsonDeserializer();
                
                cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);
            });
        });
    }
}