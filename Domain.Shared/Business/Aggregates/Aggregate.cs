using Domain.Shared.Business.UniqueIdValueObjects;
using Domain.Shared.Events.DomainEvents;

namespace Domain.Shared.Business.Aggregates;

public abstract class Aggregate<TId> : Entity, IAggregate where TId : IUniqueId
{
    public TId UniqueId { get; }
    
    private List<IDomainEvent> _domainEvents;

    public IReadOnlyCollection<IDomainEvent> DomainEvents =>
        _domainEvents.AsReadOnly();

    protected Aggregate()
    {
        this._domainEvents = _domainEvents ?? new List<IDomainEvent>();
    }

    protected Aggregate(TId uniqueId)
    {
        this._domainEvents = _domainEvents ?? new List<IDomainEvent>();
        UniqueId = uniqueId;
    } 
    
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        this._domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => this._domainEvents?.Clear();
}