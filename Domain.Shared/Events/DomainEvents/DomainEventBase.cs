using Domain.Shared.Business.UniqueIdValueObjects;

namespace Domain.Shared.Events.DomainEvents;

public abstract class DomainEventBase<TSimplifiedEvent> : IDomainEvent, ISimplifiedEventFactory<TSimplifiedEvent>
    where TSimplifiedEvent : class
{
    protected DomainEventBase(UniqueIdBase uniqueId)
    {
        UniqueId = uniqueId;
        this.OccuredOn = DateTime.Now;
    }

    public IUniqueId UniqueId { get; }
    public DateTime OccuredOn { get; }
    
    public object GenerateSipmlifiedEvent()
    {
        return GenerateEvent();
    }
    public abstract TSimplifiedEvent GenerateEvent();
}