using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.Processing.MassTransitExtensions.Filters;

public class LoggingFilter<TMessage> : IFilter<ConsumeContext<TMessage>>
    where TMessage : class
{
    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        var serviceProvider = context.GetPayload<IServiceProvider>();

        ILogger<TMessage> logger = serviceProvider.GetService<ILogger<TMessage>>();
        
        logger.LogInformation($"Request: {context.Message}");

        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("logging");
    }
}

public class LoggingConsumerConfigurationObserver : IConsumerConfigurationObserver
{
    public void ConsumerConfigured<TConsumer>(IConsumerConfigurator<TConsumer> configurator) where TConsumer : class {}

    public void ConsumerMessageConfigured<TConsumer, TMessage>(IConsumerMessageConfigurator<TConsumer, TMessage> configurator) where TConsumer : class where TMessage : class
    {
        configurator.Message(x => x.UseFilter(new LoggingFilter<TMessage>()));
    }
}