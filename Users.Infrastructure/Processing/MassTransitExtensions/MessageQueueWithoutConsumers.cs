using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Users.Infrastructure.Processing.MassTransitExtensions;

public static class MessageQueueWithoutConsumers
{
    public static void RegisterMessageQueueWithoutConsumers(this IServiceCollection services, RabbitMqConfig rabbitMqConfig)
    {
        services.AddMassTransit(config =>
        {
            config.AddHealthChecks();

            config.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = (check) => check.Tags.Contains("ready");
            });

            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqConfig.Host, rabbitMqConfig.Port, rabbitMqConfig.VirtualHost, rabbitConfig =>
                {
                    rabbitConfig.Username(rabbitMqConfig.Username);
                    rabbitConfig.Password(rabbitMqConfig.Password);
                });
                
                cfg.ClearSerialization();
                cfg.UseRawJsonSerializer();
                
                cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);
            });
        });
    }
}