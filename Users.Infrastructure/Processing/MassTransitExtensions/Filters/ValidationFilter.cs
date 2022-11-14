using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.Processing.MassTransitExtensions.Filters;

public class ValidationFilter<TMessage> : IFilter<ConsumeContext<TMessage>>
    where TMessage : class
{
    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        var serviceProvider = context.GetPayload<IServiceProvider>();

        var t = context.Message.GetType();
        
        IEnumerable<IValidator<TMessage>> validators = serviceProvider.GetService<IEnumerable<IValidator<TMessage>>>();

        IValidationContext validationContext = new ValidationContext<TMessage>(context.Message);
        
        if (validators.Any())
        {
            var validationResults =
                await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(validationContext, context.CancellationToken)));

            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures);
            }
        }

        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("logging");
    }
}

public class ValidationConsumerConfigurationObserver : IConsumerConfigurationObserver
{
    public void ConsumerConfigured<TConsumer>(IConsumerConfigurator<TConsumer> configurator) where TConsumer : class {}

    public void ConsumerMessageConfigured<TConsumer, TMessage>(IConsumerMessageConfigurator<TConsumer, TMessage> configurator) where TConsumer : class where TMessage : class
    {
        configurator.Message(x => x.UseFilter(new ValidationFilter<TMessage>()));
    }
}