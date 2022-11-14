using Domain.Shared.Commands;
using Domain.Shared.Queries;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Processing.MassTransitExtensions.Filters;

namespace Users.Infrastructure.Processing.MassTransitExtensions;

public static class MediatorWithConsumers
{
    public static void RegisterMediatorWithConsumers(this IServiceCollection services)
    {
        var commandType = typeof(ICommandHandler<>);
        var queryType = typeof(IQueryHandler<,>);

        var queryTypes = queryType.FindAllInplementationTypesByType();
        var commandTypes = commandType.FindAllInplementationTypesByType();
        
        services.AddMediator(config =>
        {
            config.ConfigureMediator((context, cfg) =>
            {
                cfg.ConnectConsumerConfigurationObserver(new LoggingConsumerConfigurationObserver());
                cfg.ConnectConsumerConfigurationObserver(new ValidationConsumerConfigurationObserver());
                
                //cfg.UseConsumeFilter(typeof(ValidationFilter<>), context);
            });
            
            config.AddConsumers(commandTypes.ToArray());
            config.AddConsumers(queryTypes.ToArray());
        });
    }
}