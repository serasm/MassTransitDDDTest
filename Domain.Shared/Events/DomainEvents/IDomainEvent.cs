using Domain.Shared.Business.UniqueIdValueObjects;

namespace Domain.Shared.Events.DomainEvents;

public interface IDomainEvent
{
    IUniqueId UniqueId { get; }
    DateTime OccuredOn { get; }

    object GenerateSipmlifiedEvent();
}