using MassTransit;

namespace Domain.Shared.Events.DomainEvents;

public abstract class DomainEventHandlerBase<TDomainMessage> : IDomainEventHandler<TDomainMessage> 
    where TDomainMessage : class
{
    public async Task Consume(ConsumeContext<TDomainMessage> context)
    {
        await HandleEventAsync(context);
    }

    public abstract Task HandleEventAsync(ConsumeContext<TDomainMessage> context);
}