using MassTransit;

namespace Domain.Shared.Events.DomainEvents;

public interface IDomainEventHandler<in TDomainMessage> : IConsumer<TDomainMessage> 
    where TDomainMessage : class
{
    
}