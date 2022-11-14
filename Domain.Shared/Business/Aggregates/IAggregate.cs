using Domain.Shared.Events.DomainEvents;

namespace Domain.Shared.Business.Aggregates;

public interface IAggregate
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}